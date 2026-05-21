# Figure Generation

Each example page can display one or more inline SVG diagrams — called
**figures** — that appear as a visual banner between the language-tab section
and the runner output.  Figures are generated entirely in C# at site-build
time; no external image files, no JavaScript, and no dependencies beyond what
is already in the project.

The system is a C# port of the [marginalia](https://github.com/adewale/pythonbyexample/blob/main/src/marginalia_grammar.py)
module from [pythonbyexample](https://github.com/adewale/pythonbyexample).

---

## Architecture overview

```
Marginalia/
├── Canvas.cs            # SVG drawing primitives – the "grammar"
├── Figures.cs           # Named paint functions + figure registry
└── FigureAttachments.cs # Slug → figure(s) attachment map
```

| File | Responsibility |
|------|---------------|
| `Canvas.cs` | A stateful SVG builder. Exposes locked drawing methods (lines, boxes, arrows, text). No raw SVG is ever written outside this file. |
| `Figures.cs` | One static method per named figure, plus a `Registry` dictionary that maps a figure name string to its paint method and canvas dimensions. |
| `FigureAttachments.cs` | Maps example-directory slugs to one or more `FigureAttachment` records (anchor, figure name, caption). |

`Nocco.cs` calls `FigureAttachments.GetFigures(slug)` for every example being
generated and passes the resulting `(svg, caption)` pairs to the Razor
template.  `Webpage.razor` renders a `<div class="cell-banner">` when the list
is non-empty.  The CSS lives in `Resources/nocco.css` under the `Figure
Banners` comment block.

---

## End-to-end pipeline

```
dotnet run
  │
  ├─ for each examples/<slug>/
  │     │
  │     ├─ FigureAttachments.GetFigures(slug)
  │     │     │
  │     │     └─ looks up Attachments["slug"]
  │     │           for each FigureAttachment:
  │     │             Figures.RenderSvg(figureName)
  │     │               └─ creates Canvas(w, h)
  │     │                   calls paint function on it
  │     │                   returns canvas.ToSvg()
  │     │
  │     └─ Webpage.razor receives List<(Svg, Caption)>
  │           renders <div class="cell-banner"> if list non-empty
  │
  └─ site/<slug>.html written to disk
```

---

## The visual language

`Canvas` enforces a **locked** visual vocabulary — paint functions cannot pick
colours or stroke weights freely.

### Palette

| Constant | Colour | Usage |
|----------|--------|-------|
| `Ink` | `#521000` (warm dark brown) | All lines, boxes, arrows, and text |
| `InkSoft` | `rgba(82,16,0, 0.7)` | Tags, labels, captions |
| `Emphasis` | `#FF4801` (orange) | **One mark per figure only** — the single most important element |
| `SoftFill` | `rgba(82,16,0, 0.05)` | Quiet tinted fill for "live" boxes |

> **Emphasis scarcity rule:** use `emphasis: true` on at most one arrow, dot,
> or cell per figure.  Multiple orange marks break the "this one thing
> matters" communication.

### Drawing primitives

| Method | Shape |
|--------|-------|
| `Hairline(x1,y1,x2,y2)` | Thin line (0.6 weight) |
| `Stroke(x1,y1,x2,y2)` | Standard line (1.0 weight) |
| `Dashed(x1,y1,x2,y2)` | Dashed hairline |
| `Ghost(x1,y1,x2,y2)` | Faded line (40% opacity) |
| `Tick(x,y)` | Short vertical tick |
| `Dot(x,y)` | Small filled circle |
| `Register(x,y,w, divisions:n)` | Horizontal number-line with n ticks |
| `Tag(x,y,s)` | Extra-small uppercase label |
| `Label(x,y,s)` | Small muted label |
| `Mono(x,y,s)` | Monospaced text |
| `Ident(x,y,s)` | Serif italic identifier |
| `NameBox(x,y,name)` | Open rect with italic name, returns right-edge midpoint |
| `ObjectBox(x,y,typeTag,value)` | Filled rect with type tag above and value centred, returns left-edge midpoint |
| `CellBox(x,y,content)` | Plain box with centred text |
| `Cells(x,y,items[])` | Row of adjacent cells |
| `Frame(x,y,w,h)` | Open rect with optional tag label |
| `Node(x,y,label)` | Circle with centred label |
| `OpenArrow(x1,y1,x2,y2)` | Hairline + open-V arrowhead |
| `ClosedArrow(x1,y1,x2,y2)` | Line + filled wedge arrowhead |
| `Bind(x,y,name,typeTag,value)` | The name→object phrase (NameBox + ClosedArrow + ObjectBox) |

### Canvas size and viewBox

Pass explicit `w` and `h` when registering a figure (see [Step 4](#step-4--register-the-figure)):

* `w` and `h` are in **viewBox units** — the internal coordinate space.
* `Canvas.ToSvg()` adds 14-unit padding on all sides automatically so nothing
  gets clipped.
* The SVG is rendered at **1.6×** its viewBox dimensions so it looks larger on
  desktop while still shrinking to fit narrow viewports via `max-width: 100%`.

Start with the minimum size that fits your marks, then adjust.  Too much
whitespace weakens the diagram.

---

## How to add a figure — step by step

There are three files to edit and nothing else:

1. Write a paint function in `Figures.cs`
2. Register it in the `Registry` dictionary in `Figures.cs`
3. Attach it to an example in `FigureAttachments.cs`

Then run the site generator to verify the output.

---

### Step 1 — Sketch the diagram

Before writing code, sketch what you want to show on paper or a whiteboard.
Keep it to **one idea**.  A figure that tries to show three things at once
teaches nothing.

Ask:
* What is the single take-away for the reader?
* Which canvas primitives best express it?
* Where should the one `emphasis` mark go to name the most important element?

---

### Step 2 — Write the paint function

Open `src/dotnetbyexample/Marginalia/Figures.cs` and add a `static void`
method.  The method receives a `Canvas` and calls drawing methods on it.

**Rules:**
* No `if (someExternalFlag)` — the function is deterministic.
* No colours outside the palette (the palette is baked into `Canvas`).
* Coordinates are in viewBox units.  The top-left is `(0, 0)`.  y increases
  downwards.

**Example — `ForLoop`:**

```csharp
/// <summary>
/// For loops · a counter advances one cell at a time; the loop stops when
/// the range is exhausted.
/// </summary>
public static void ForLoop(Canvas c)
{
    // Draw four cells representing the sequence items
    c.Cells(0, 10, new[] { "0", "1", "2", "3" }, w: 28, h: 24);

    // Draw a dot above the third cell (index 2) as the current position
    c.Dot(0 + 2 * 28 + 14, 6, emphasis: true);

    // Label: range end
    c.Tag(0, 48, "range(4)");
    c.Label(0 + 4 * 28 + 6, 22, "exhausted → stop");
    // ^ x = start(0) + four cells(4×28=112) + 6-unit gap = 118
}
```

Tip: work out x/y by hand with a ruler, or iterate quickly by building and
opening the generated HTML.

---

### Step 3 — Choose the canvas size

The canvas size is the bounding box of your marks in viewBox units.  Set `w`
and `h` so the content fits without excessive padding.

For the `ForLoop` example:
* Width: four 28-unit cells = 112, plus the "stop here" label ~100 extra = **210**
* Height: tag at y=6 (height ~8), cells at y=14 (height 24), label inside cells ≈ **46**

Start with a round number, build, look, and adjust.

---

### Step 4 — Register the figure

Still in `Figures.cs`, add an entry to the `Registry` dictionary:

```csharp
["for-loop"] = (ForLoop, 210, 46),
//              ^paint    ^w   ^h
```

The key is the **figure name** — a kebab-case string used to reference the
figure from `FigureAttachments.cs`.  Choose a name that is unique and
descriptive.

---

### Step 5 — Attach the figure to an example

Open `src/dotnetbyexample/Marginalia/FigureAttachments.cs` and add an entry to
the `Attachments` dictionary.

The **key** is the example's **slug** — the directory name under `examples/`
in lowercase (e.g. the directory `examples/for` → slug `"for"`).

```csharp
["for"] = new[]
{
    new FigureAttachment(
        "after-last",
        "for-loop",
        "The loop counter advances through each item in the range; the loop stops automatically when the range is exhausted."),
},
```

| Field | Value |
|-------|-------|
| `Anchor` | `"after-last"` (currently the only supported anchor — renders the banner after the last code section) |
| `FigureName` | Must match the key you added to `Figures.Registry` |
| `Caption` | One sentence describing what the figure shows.  Written as prose, not a title. |

---

### Step 6 — Build and verify

```bash
# From the repository root:
dotnet run --project src/dotnetbyexample/

# Open the generated page:
# site/<slug>.html
```

Check:
- [ ] The figure banner appears between the language tabs and the runner output.
- [ ] No marks are clipped (increase `h` in the registry if the bottom is cut off).
- [ ] The `emphasis` orange mark is the most important element.
- [ ] The caption reads as a complete sentence.

---

## Full worked example — `for-loop`

This section walks through adding a `for-loop` figure to the `for` example
page, from scratch.

### What does the `for` example show?

The `for` example demonstrates iterating over a sequence with a counter.  The
key mental model: a position advances through a fixed-size range one step at a
time, stopping when the end is reached.

### The figure plan

```
 0   1   2   3
┌──┬──┬──┬──┐
│  │  │● │  │   ← orange dot = current position
└──┴──┴──┴──┘
range(4)         label
```

A row of cells (the range), an orange dot marking the current index, and a
label naming the construct.

### Step 1 — Paint function (add to `Figures.cs`)

```csharp
/// <summary>
/// For loops · a counter advances one cell at a time; the loop stops when
/// the range is exhausted.
/// </summary>
public static void ForLoop(Canvas c)
{
    c.Tag(0, 6, "for i in range(4)");
    c.Cells(0, 14, new[] { "0", "1", "2", "3" }, w: 28, h: 24);
    // Orange dot above cell index 2 (x = 2*28 + 14 = 70) — current position
    c.Dot(70, 10, emphasis: true);
    c.Label(118, 26, "← stop here");
}
```

### Step 2 — Registry entry (add to `Figures.Registry` in `Figures.cs`)

```csharp
["for-loop"] = (ForLoop, 210, 46),
```

### Step 3 — Attachment (add to `FigureAttachments.Attachments` in `FigureAttachments.cs`)

```csharp
["for"] = new[]
{
    new FigureAttachment(
        "after-last",
        "for-loop",
        "A for loop advances a counter through each value in the range one step at a time; the loop stops automatically when the range is exhausted."),
},
```

### Step 4 — Run and inspect

```bash
dotnet run --project src/dotnetbyexample/
# open site/for.html
```

The banner will appear with the SVG figure above and the caption beneath it in
italic muted text.

---

## Full worked example — `struct-fields`

This second example walks through adding a `struct-fields` figure to the
`structs` example page.  It demonstrates using `Frame`, `NameBox`, `ObjectBox`,
and `ClosedArrow` together to picture a record type.

### What does the `structs` example show?

A struct groups named, typed fields into a single composite value.  The key
mental model: a `Person` is a labelled container holding two independent slots
— `Name` (string) and `Age` (int) — each of which can be read or updated by
name.

### The figure plan

```
 ┌─ Person ──────────────────────────────┐
 │                                       │
 │  ┌────────┐       ┌─────────────────┐ │
 │  │  Name  │  ──►  │ STRING  "Bob"   │ │
 │  └────────┘       └─────────────────┘ │
 │                                       │
 │  ┌────────┐      →┌─────────────────┐ │
 │  │  Age   │  ──►  │ INT     20      │ │ ← emphasis arrow
 │  └────────┘       └─────────────────┘ │
 │                                       │
 └───────────────────────────────────────┘
```

A `Frame` wraps the whole struct; `NameBox` shows each field name; `ObjectBox`
shows the type tag and value; `ClosedArrow` connects them.  The orange arrow
highlights the `Age` field to draw the reader's attention to the value-type
nature of the field.

### Step 1 — Paint function (add to `Figures.cs`)

```csharp
/// <summary>
/// Structs · a struct groups named, typed fields into a single record.
/// </summary>
public static void StructFields(Canvas c)
{
    // Outer frame labelled with the type name
    c.Frame(0, 0, 206, 96, label: "Person");

    // Row 1 — Name field
    var (nx1, ny1) = c.NameBox(10, 19, "Name");
    var (ox1, oy1) = c.ObjectBox(nx1 + 14, 14, "string", "\"Bob\"", w: 80, h: 34, tagPosition: "inside");
    c.ClosedArrow(nx1 + 2, ny1, ox1 - 2, oy1);

    // Row 2 — Age field (emphasis arrow)
    var (nx2, ny2) = c.NameBox(10, 59, "Age");
    var (ox2, oy2) = c.ObjectBox(nx2 + 14, 54, "int", "20", w: 80, h: 34, tagPosition: "inside");
    c.ClosedArrow(nx2 + 2, ny2, ox2 - 2, oy2, emphasis: true);
}
```

**Sizing note:** Using `tagPosition: "inside"` with `h: 34` leaves the type
tag 10 units from the top of the box and the mono value centred at 21 units
in — enough breathing room for both labels.  Aligning `NameBox(y: 19)` at
`y_obj + (h - NameH) / 2 = 14 + 5 = 19` makes the name box vertically centred
on the object box, so arrows come out perfectly horizontal.

### Step 2 — Registry entry (add to `Figures.Registry` in `Figures.cs`)

```csharp
["struct-fields"] = (StructFields, 206, 96),
```

### Step 3 — Attachment (add to `FigureAttachments.Attachments` in `FigureAttachments.cs`)

```csharp
["structs"] = new[]
{
    new FigureAttachment(
        "after-last",
        "struct-fields",
        "A struct groups named, typed fields into a single record; each field holds its own value independently."),
},
```

### Step 4 — Run and inspect

```bash
dotnet run --project src/dotnetbyexample/
# open site/structs.html
```

The generated page looks like this, with the figure banner appearing between
the last code section and the runner output:

![structs example page showing the Person struct figure banner](figure-structs.png)

---

## Gallery — figures added in v2

Four additional figures were added to cover the `if-else`, `maps`, `slices`,
and `pointers` examples.

### `if-else` — IfElseBranch

A condition box at the top with two branching arrows pointing to the `true`
and `false` outcome boxes.  The emphasis (orange) arrow highlights the `false`
branch to draw attention to the else path.

```csharp
["if-else-branch"] = (IfElseBranch, 220, 82),
```

![if-else example page showing the branching figure banner](figure-if-else.png)

### `maps` — MapEntries

Two key→value pairs using `ObjectBox` for both sides.  Column tags `KEY` and
`VALUE` sit above the rows.  The orange arrow on the second pair emphasises
that every lookup yields one value.

```csharp
["map-entries"] = (MapEntries, 180, 70),
```

![maps example page showing the key-value figure banner](figure-maps.png)

### `slices` — SliceWindow

A row of five cells (following the `ForLoop` pattern) with an orange dot above
cell index 1 — the start of the `GetRange(1, 2)` sub-view — and a label
naming the result.

```csharp
["slice-window"] = (SliceWindow, 150, 54),
```

![slices example page showing the sub-slice figure banner](figure-slices.png)

### `pointers` — RefAlias

Two `NameBox` entries (`i` and `iptr`) with arrows converging on a single
`ObjectBox`.  The orange arrow on `iptr` makes the aliasing relationship the
focal point of the diagram.

```csharp
["ref-alias"] = (RefAlias, 165, 66),
```

![pointers example page showing the ref-alias figure banner](figure-pointers.png)

---

## Gallery — figures added in v3

Four additional figures covering `arrays`, `switch`, `functions`, and
`multiple-return-values`.

### `arrays` — ArrayIndex

A five-cell row labelled with values `{1,2,3,4,5}`.  An orange dot above the
last cell marks the element being accessed by `b[4]`; a small label below
confirms the value `= 5`.

```csharp
["array-index"] = (ArrayIndex, 155, 52),
```

![arrays example page showing the array-index figure banner](figure-arrays.png)

### `switch` — SwitchCases

A switch-value box at the top with three fanning-out arrows to three outcome
boxes.  The orange arrow on the centre arm (`case 2`) highlights the matching
branch when `i = 2`.

```csharp
["switch-cases"] = (SwitchCases, 205, 84),
```

![switch example page showing the switch-cases figure banner](figure-switch.png)

### `functions` — FunctionCall

A call box on the left with an orange arrow pointing at a typed result box on
the right, annotated `returns int`.  Shows that a function takes arguments and
hands back one return value.

```csharp
["function-call"] = (FunctionCall, 200, 62),
```

![functions example page showing the function-call figure banner](figure-functions.png)

### `multiple-return-values` — MultiReturn

A single `Vals()` call box fans out to two stacked `int` result boxes.  The
orange arrow on the lower result emphasises that both values are produced by
the one call and deconstructed by the caller.

```csharp
["multi-return"] = (MultiReturn, 175, 72),
```

![multiple-return-values example page showing the multi-return figure banner](figure-multiple-return-values.png)

---

## Gallery — figures added in v4

Seventeen figures covering `hello-world`, `constants`, `enums`, `methods`,
`interfaces`, `errors`, `generics`, `sorting`, `variadic-functions`, `defer`,
`goroutines`, `channels`, `timers`, `url-parsing`, `json`, `string-formatting`,
and `atomic-counters`.

### `hello-world` — HelloWorldPrint

A single `Console.WriteLine` call box with an arrow to the terminal output box.

```csharp
["console-writeline"] = (HelloWorldPrint, 272, 46),
```

![hello-world example page showing the console-writeline figure banner](figure-hello-world.png)

### `constants` — ConstValue

A constant box labelled with its type and value, indicating compile-time fixity.

```csharp
["const-value"] = (ConstValue, 240, 42),
```

![constants example page showing the const-value figure banner](figure-constants.png)

### `enums` — EnumValues

A row of labelled enum member cells showing their underlying integer values.

```csharp
["enum-values"] = (EnumValues, 195, 42),
```

![enums example page showing the enum-values figure banner](figure-enums.png)

### `methods` — MethodSlots

A type box with two method-slot boxes extending from it, showing instance-method dispatch.

```csharp
["method-slots"] = (MethodSlots, 180, 90),
```

![methods example page showing the method-slots figure banner](figure-methods.png)

### `interfaces` — InterfaceContract

An interface box connected by an arrow to two concrete-type boxes satisfying the contract.

```csharp
["interface-contract"] = (InterfaceContract, 220, 90),
```

![interfaces example page showing the interface-contract figure banner](figure-interfaces.png)

### `errors` — ExceptionFlow

A try block connected by two diverging arrows — one to a success path and one to a catch handler.

```csharp
["exception-flow"] = (ExceptionFlow, 212, 88),
```

![errors example page showing the exception-flow figure banner](figure-errors.png)

### `generics` — GenericBox

A generic container `Box<T>` holding a concrete value, illustrating the type parameter.

```csharp
["generic-box"] = (GenericBox, 212, 84),
```

![generics example page showing the generic-box figure banner](figure-generics.png)

### `sorting` — SortBeforeAfter

A before/after pair of cell rows showing unsorted values on the left and sorted values on the right.

```csharp
["sort-before-after"] = (SortBeforeAfter, 115, 92),
```

![sorting example page showing the sort-before-after figure banner](figure-sorting.png)

### `variadic-functions` — ParamsArray

A `params` call box with a spread of three argument cells entering the function.

```csharp
["params-array"] = (ParamsArray, 215, 42),
```

![variadic-functions example page showing the params-array figure banner](figure-variadic-functions.png)

### `defer` — LifoDefer

A stack of deferred calls executed in last-in-first-out order as the function returns.

```csharp
["lifo-defer"] = (LifoDefer, 168, 88),
```

![defer example page showing the lifo-defer figure banner](figure-defer.png)

### `goroutines` — GoroutineLanes

Two goroutine lanes running concurrently beside the main goroutine lane.

```csharp
["goroutine-lanes"] = (GoroutineLanes, 215, 76),
```

![goroutines example page showing the goroutine-lanes figure banner](figure-goroutines.png)

### `channels` — ChannelBuffer

A sender box pushing a value into a channel buffer, then a receiver box draining it.

```csharp
["channel-buffer"] = (ChannelBuffer, 215, 42),
```

![channels example page showing the channel-buffer figure banner](figure-channels.png)

### `timers` — TimerTick

A timer fires a single tick after its duration; a tag marks the moment the channel unblocks.

```csharp
["timer-tick"] = (TimerTick, 205, 46),
```

![timers example page showing the timer-tick figure banner](figure-timers.png)

### `url-parsing` — UrlParts

A URL string decomposed into scheme, host, path, and query-string parts.

```csharp
["url-parts"] = (UrlParts, 270, 36),
```

![url-parsing example page showing the url-parts figure banner](figure-url-parsing.png)

### `json` — JsonSerialize

An object box serialised through a JSON encoder to a string, with a back-arrow for deserialization.

```csharp
["json-serialize"] = (JsonSerialize, 236, 52),
```

![json example page showing the json-serialize figure banner](figure-json.png)

### `string-formatting` — FormatPlaceholders

A format-string template box with numbered placeholders matched to argument boxes below.

```csharp
["format-placeholders"] = (FormatPlaceholders, 200, 86),
```

![string-formatting example page showing the format-placeholders figure banner](figure-string-formatting.png)

### `atomic-counters` — AtomicCounterSync

Multiple goroutine lanes each calling `Interlocked.Increment` on a single shared counter.

```csharp
["atomic-counter-sync"] = (AtomicCounterSync, 210, 78),
```

![atomic-counters example page showing the atomic-counter-sync figure banner](figure-atomic-counters.png)

---

## Gallery — figures added in v5

Seventeen figures covering `base64-encoding`, `channel-directions`,
`channel-synchronization`, `closing-channels`, `command-line-arguments`,
`command-line-flags`, `context`, `custom-errors`, `environment-variables`,
`epoch`, `logging`, `mutexes`, `number-parsing`, `panic`,
`range-over-built-in-types`, `channel-buffering`, and `command-line-subcommands`.

### `base64-encoding` — Base64Roundtrip

Raw bytes encoded to a Base64 string and decoded back to the original bytes.

```csharp
["base64-roundtrip"] = (Base64Roundtrip, 296, 44),
```

![base64-encoding example page showing the base64-roundtrip figure banner](figure-base64-encoding.png)

### `channel-directions` — ChannelDirections

Two directional channel variants — send-only and receive-only — shown as typed arrows.

```csharp
["channel-directions"] = (ChannelDirections, 240, 42),
```

![channel-directions example page showing the channel-directions figure banner](figure-channel-directions.png)

### `channel-synchronization` — ChannelSync

A goroutine sends a done signal over a channel; the main goroutine blocks until the signal arrives.

```csharp
["channel-sync"] = (ChannelSync, 190, 84),
```

![channel-synchronization example page showing the channel-sync figure banner](figure-channel-synchronization.png)

### `closing-channels` — ChannelCloseRange

A producer closes the channel after sending all values; the consumer drains until the channel is empty.

```csharp
["channel-close-range"] = (ChannelCloseRange, 202, 60),
```

![closing-channels example page showing the channel-close-range figure banner](figure-closing-channels.png)

### `command-line-arguments` — CliArgsSlice

The `os.Args` slice showing the program name at index 0 and user arguments at index 1+.

```csharp
["cli-args-slice"] = (CliArgsSlice, 134, 48),
```

![command-line-arguments example page showing the cli-args-slice figure banner](figure-command-line-arguments.png)

### `command-line-flags` — CliFlagsParse

A flag definition box parsed from the command-line string to a typed value.

```csharp
["cli-flags-parse"] = (CliFlagsParse, 204, 42),
```

![command-line-flags example page showing the cli-flags-parse figure banner](figure-command-line-flags.png)

### `context` — ContextCancelTree

A parent context with a cancel function propagating cancellation down to child contexts.

```csharp
["context-cancel-tree"] = (ContextCancelTree, 200, 78),
```

![context example page showing the context-cancel-tree figure banner](figure-context.png)

### `custom-errors` — CustomErrorFields

A custom error struct carrying named fields such as argument name and message.

```csharp
["custom-error-fields"] = (CustomErrorFields, 210, 86),
```

![custom-errors example page showing the custom-error-fields figure banner](figure-custom-errors.png)

### `environment-variables` — EnvKeyValues

A row of key/value pairs representing process-scoped environment variables.

```csharp
["env-key-values"] = (EnvKeyValues, 184, 44),
```

![environment-variables example page showing the env-key-values figure banner](figure-environment-variables.png)

### `epoch` — EpochOffset

A time value shown as a numeric offset from the Unix epoch (1970-01-01T00:00:00Z).

```csharp
["epoch-offset"] = (EpochOffset, 222, 56),
```

![epoch example page showing the epoch-offset figure banner](figure-epoch.png)

### `logging` — StructuredLogEntry

A structured log record with named fields: level, timestamp, and message.

```csharp
["structured-log-entry"] = (StructuredLogEntry, 218, 68),
```

![logging example page showing the structured-log-entry figure banner](figure-logging.png)

### `mutexes` — MutexCriticalSection

A mutex lock/unlock pair bracketing the critical-section box, enforcing exclusive access.

```csharp
["mutex-critical"] = (MutexCriticalSection, 250, 54),
```

![mutexes example page showing the mutex-critical figure banner](figure-mutexes.png)

### `number-parsing` — NumberParseFlow

A text input passing through a parse function to a success/failure branch.

```csharp
["number-parse-flow"] = (NumberParseFlow, 220, 64),
```

![number-parsing example page showing the number-parse-flow figure banner](figure-number-parsing.png)

### `panic` — PanicRecoverStack

A panic unwinds the call stack through multiple frames; a deferred recover intercepts the unwind.

```csharp
["panic-recover-stack"] = (PanicRecoverStack, 192, 82),
```

![panic example page showing the panic-recover-stack figure banner](figure-panic.png)

### `range-over-built-in-types` — RangeRunes

A string shown as UTF-8 bytes on the left and decoded rune/index pairs on the right.

```csharp
["range-runes"] = (RangeRunes, 86, 50),
```

![range-over-built-in-types example page showing the range-runes figure banner](figure-range-over-built-in-types.png)

### `channel-buffering` — ChannelBuffering

A buffered channel with capacity 3; the sender fills the buffer without blocking the receiver.

```csharp
["channel-buffering-cap"] = (ChannelBuffering, 200, 54),
```

![channel-buffering example page showing the channel-buffering-cap figure banner](figure-channel-buffering.png)

### `command-line-subcommands` — CliSubcommands

A root dispatcher box routing to named sub-command boxes based on the first argument.

```csharp
["cli-subcommands"] = (CliSubcommands, 154, 92),
```

![command-line-subcommands example page showing the cli-subcommands figure banner](figure-command-line-subcommands.png)

---

## Gallery — figures added in v6

Seventeen figures covering `directories`, `embed-directive`, `execing-processes`,
`exit`, `file-paths`, `http-client`, `http-server`, `line-filters`,
`non-blocking-channel-operations`, `random-numbers`, `range-over-channels`,
`range-over-iterators`, `rate-limiting`, `reading-files`, and the remaining
batch-3 examples.

### `directories` — DirectoryTree

A tree of parent and child directory nodes with file leaves at the bottom.

```csharp
["directory-tree"] = (DirectoryTree, 180, 108),
```

![directories example page showing the directory-tree figure banner](figure-directories.png)

### `embed-directive` — EmbedDirective

Source-embedded assets bundled into the binary alongside the `//go:embed` directive.

```csharp
["embed-directive"] = (EmbedDirective, 218, 58),
```

![embed-directive example page showing the embed-directive figure banner](figure-embed-directive.png)

### `execing-processes` — ExecProcess

The current process image replaced by a new executable while retaining the same PID.

```csharp
["exec-process"] = (ExecProcess, 228, 54),
```

![execing-processes example page showing the exec-process figure banner](figure-execing-processes.png)

### `exit` — ExitStatus

A program terminating and returning a numeric status code to the shell.

```csharp
["exit-status"] = (ExitStatus, 180, 54),
```

![exit example page showing the exit-status figure banner](figure-exit.png)

### `file-paths` — FilePathParts

A file path split into directory prefix, base name, and extension parts.

```csharp
["file-path-parts"] = (FilePathParts, 190, 44),
```

![file-paths example page showing the file-path-parts figure banner](figure-file-paths.png)

### `http-client` — HttpClientRoundtrip

A client sending a GET request and receiving a 200 OK response from a server.

```csharp
["http-client-roundtrip"] = (HttpClientRoundtrip, 218, 48),
```

![http-client example page showing the http-client-roundtrip figure banner](figure-http-client.png)

### `http-server` — HttpServerListener

A multiplexer accepting connections and dispatching each request to a registered handler.

```csharp
["http-server-listener"] = (HttpServerListener, 240, 60),
```

![http-server example page showing the http-server-listener figure banner](figure-http-server.png)

### `line-filters` — LineFilter

Stdin lines passing through a transform step and written to stdout.

```csharp
["line-filter"] = (LineFilter, 270, 44),
```

![line-filters example page showing the line-filter figure banner](figure-line-filters.png)

### `non-blocking-channel-operations` — NonBlockingSelect

A select with a default clause falling through immediately when no channel is ready.

```csharp
["non-blocking-select"] = (NonBlockingSelect, 174, 64),
```

![non-blocking-channel-operations example page showing the non-blocking-select figure banner](figure-non-blocking-channel-operations.png)

### `random-numbers` — RandomNumbers

A seeded RNG producing a different sequence of values on every run.

```csharp
["random-numbers"] = (RandomNumbers, 200, 54),
```

![random-numbers example page showing the random-numbers figure banner](figure-random-numbers.png)

### `range-over-channels` — RangeOverChannels

A range loop reading from a channel until the channel is closed.

```csharp
["range-over-channels"] = (RangeOverChannels, 256, 44),
```

![range-over-channels example page showing the range-over-channels figure banner](figure-range-over-channels.png)

### `range-over-iterators` — RangeIterator

A custom iterator yielding successive values into a range loop body.

```csharp
["range-iterator"] = (RangeIterator, 180, 56),
```

![range-over-iterators example page showing the range-iterator figure banner](figure-range-over-iterators.png)

### `rate-limiting` — RateLimiter

A ticker releasing one token per interval, gating request processing to the allowed rate.

```csharp
["rate-limiter"] = (RateLimiter, 202, 66),
```

![rate-limiting example page showing the rate-limiter figure banner](figure-rate-limiting.png)

### `reading-files` — ReadFileBuffer

File bytes read from disk into a memory buffer with a single I/O call.

```csharp
["read-file-buffer"] = (ReadFileBuffer, 186, 60),
```

![reading-files example page showing the read-file-buffer figure banner](figure-reading-files.png)

---

## Gallery — figures added in final batch

Twenty-two figures completing full figure coverage across all 84 examples.

### `recover` — RecoverFlow

A deferred recover call intercepting a panic while the call stack unwinds.

```csharp
["recover-flow"] = (RecoverFlow, 210, 88),
```

![recover example page showing the recover-flow figure banner](figure-recover.png)

### `regular-expressions` — RegexCaptures

A regex pattern applied to input text with capture groups extracted.

```csharp
["regex-captures"] = (RegexCaptures, 260, 54),
```

![regular-expressions example page showing the regex-captures figure banner](figure-regular-expressions.png)

### `select` — SelectRace

Multiple channel cases racing in a select; the first-ready case wins.

```csharp
["select-race"] = (SelectRace, 236, 84),
```

![select example page showing the select-race figure banner](figure-select.png)

### `sha256-hashes` — Sha256Digest

Input bytes transformed by SHA256 into a fixed-length 32-byte digest.

```csharp
["sha256-digest"] = (Sha256Digest, 250, 44),
```

![sha256-hashes example page showing the sha256-digest figure banner](figure-sha256-hashes.png)

### `signals` — SignalChannel

An OS signal delivered to the process via a buffered channel and handled cooperatively.

```csharp
["signal-channel"] = (SignalChannel, 220, 58),
```

![signals example page showing the signal-channel figure banner](figure-signals.png)

### `sorting-by-functions` — SortComparator

A custom comparator function driving the ordering of elements in a sort.

```csharp
["sort-comparator"] = (SortComparator, 210, 88),
```

![sorting-by-functions example page showing the sort-comparator figure banner](figure-sorting-by-functions.png)

### `spawning-processes` — SpawnProcessPipe

A parent process spawning a child command and reading its stdout through a pipe.

```csharp
["spawn-process-pipe"] = (SpawnProcessPipe, 260, 58),
```

![spawning-processes example page showing the spawn-process-pipe figure banner](figure-spawning-processes.png)

### `stateful-goroutines` — StatefulOwner

A single owner goroutine serialising state access; others interact via request/reply channels.

```csharp
["stateful-owner"] = (StatefulOwner, 260, 88),
```

![stateful-goroutines example page showing the stateful-owner figure banner](figure-stateful-goroutines.png)

### `string-functions` — StringOps

Common string operations (Split, Replace) applied to an input string to produce derived outputs.

```csharp
["string-ops"] = (StringOps, 270, 52),
```

![string-functions example page showing the string-ops figure banner](figure-string-functions.png)

### `strings-and-runes` — StringRuneBytes

A string's UTF-8 bytes decoded to a rune code point, showing byte vs character distinction.

```csharp
["string-rune-bytes"] = (StringRuneBytes, 250, 56),
```

![strings-and-runes example page showing the string-rune-bytes figure banner](figure-strings-and-runes.png)

### `struct-embedding` — StructEmbedding

An embedded struct's fields promoted onto the outer type, enabling composition.

```csharp
["struct-embedding"] = (StructEmbedding, 240, 84),
```

![struct-embedding example page showing the struct-embedding figure banner](figure-struct-embedding.png)

### `temp-files-and-directories` — TempFileLifecycle

A temporary path created in the OS temp directory, used for short-lived work, then cleaned up.

```csharp
["temp-file-lifecycle"] = (TempFileLifecycle, 260, 58),
```

![temp-files-and-directories example page showing the temp-file-lifecycle figure banner](figure-temp-files-and-directories.png)

### `testing-and-benchmarking` — TestBenchmarkFlow

Tests asserting correctness via pass/fail; benchmarks measuring execution speed in ns/op.

```csharp
["test-benchmark-flow"] = (TestBenchmarkFlow, 250, 84),
```

![testing-and-benchmarking example page showing the test-benchmark-flow figure banner](figure-testing-and-benchmarking.png)

### `text-templates` — TextTemplateRender

A template combining static text and action blocks with data to produce rendered output.

```csharp
["text-template-render"] = (TextTemplateRender, 250, 58),
```

![text-templates example page showing the text-template-render figure banner](figure-text-templates.png)

### `tickers` — TickerStream

A ticker emitting periodic ticks on a channel at a fixed interval until stopped.

```csharp
["ticker-stream"] = (TickerStream, 220, 58),
```

![tickers example page showing the ticker-stream figure banner](figure-tickers.png)

### `time` — TimeFields

A time value with its year, month, day and hour:min:sec components labelled.

```csharp
["time-fields"] = (TimeFields, 260, 46),
```

![time example page showing the time-fields figure banner](figure-time.png)

### `time-formatting-parsing` — TimeFormatParse

A time value formatted to a string using a layout, then parsed back to a time value.

```csharp
["time-format-parse"] = (TimeFormatParse, 276, 50),
```

![time-formatting-parsing example page showing the time-format-parse figure banner](figure-time-formatting-parsing.png)

### `timeouts` — TimeoutSelect

A select racing a work channel against a timer channel to abort after a deadline.

```csharp
["timeout-select"] = (TimeoutSelect, 238, 84),
```

![timeouts example page showing the timeout-select figure banner](figure-timeouts.png)

### `waitgroups` — WaitGroupCounter

A WaitGroup counter tracking running goroutines; Done decrements it; Wait blocks until zero.

```csharp
["waitgroup-counter"] = (WaitGroupCounter, 250, 86),
```

![waitgroups example page showing the waitgroup-counter figure banner](figure-waitgroups.png)

### `worker-pools` — WorkerPool

A fixed set of worker goroutines consuming jobs from a shared queue and emitting results.

```csharp
["worker-pool"] = (WorkerPool, 270, 88),
```

![worker-pools example page showing the worker-pool figure banner](figure-worker-pools.png)

### `writing-files` — WriteFileBuffer

In-memory bytes persisted to disk using a single write call.

```csharp
["write-file-buffer"] = (WriteFileBuffer, 250, 56),
```

![writing-files example page showing the write-file-buffer figure banner](figure-writing-files.png)

### `xml` — XmlRoundtrip

A .NET object serialised to XML and deserialised back to an equivalent object.

```csharp
["xml-roundtrip"] = (XmlRoundtrip, 272, 56),
```

![xml example page showing the xml-roundtrip figure banner](figure-xml.png)

---

## Reference — all figures

| Figure name | Paint method | Canvas (w×h) | Attached to |
|-------------|-------------|-------------|-------------|
| `number-lines` | `NumberLines` | 260×78 | `values` |
| `value-types` | `ValueTypes` | 160×120 | — |
| `call-stack` | `CallStack` | 200×100 | `recursion` |
| `closure-cell` | `ClosureCell` | 240×120 | `closures` |
| `variables-bind` | `VariablesBind` | 180×44 | `variables` |
| `for-loop` | `ForLoop` | 210×46 | `for` |
| `struct-fields` | `StructFields` | 206×96 | `structs` |
| `if-else-branch` | `IfElseBranch` | 220×82 | `if-else` |
| `map-entries` | `MapEntries` | 180×70 | `maps` |
| `slice-window` | `SliceWindow` | 150×54 | `slices` |
| `ref-alias` | `RefAlias` | 165×66 | `pointers` |
| `array-index` | `ArrayIndex` | 155×52 | `arrays` |
| `switch-cases` | `SwitchCases` | 205×84 | `switch` |
| `function-call` | `FunctionCall` | 200×62 | `functions` |
| `multi-return` | `MultiReturn` | 175×72 | `multiple-return-values` |
| `console-writeline` | `HelloWorldPrint` | 272×46 | `hello-world` |
| `const-value` | `ConstValue` | 240×42 | `constants` |
| `enum-values` | `EnumValues` | 195×42 | `enums` |
| `method-slots` | `MethodSlots` | 180×90 | `methods` |
| `interface-contract` | `InterfaceContract` | 220×90 | `interfaces` |
| `exception-flow` | `ExceptionFlow` | 212×88 | `errors` |
| `generic-box` | `GenericBox` | 212×84 | `generics` |
| `sort-before-after` | `SortBeforeAfter` | 115×92 | `sorting` |
| `params-array` | `ParamsArray` | 215×42 | `variadic-functions` |
| `lifo-defer` | `LifoDefer` | 168×88 | `defer` |
| `goroutine-lanes` | `GoroutineLanes` | 215×76 | `goroutines` |
| `channel-buffer` | `ChannelBuffer` | 215×42 | `channels` |
| `timer-tick` | `TimerTick` | 205×46 | `timers` |
| `url-parts` | `UrlParts` | 270×36 | `url-parsing` |
| `json-serialize` | `JsonSerialize` | 236×52 | `json` |
| `format-placeholders` | `FormatPlaceholders` | 200×86 | `string-formatting` |
| `atomic-counter-sync` | `AtomicCounterSync` | 210×78 | `atomic-counters` |
| `base64-roundtrip` | `Base64Roundtrip` | 296×44 | `base64-encoding` |
| `channel-directions` | `ChannelDirections` | 240×42 | `channel-directions` |
| `channel-sync` | `ChannelSync` | 190×84 | `channel-synchronization` |
| `channel-close-range` | `ChannelCloseRange` | 202×60 | `closing-channels` |
| `cli-args-slice` | `CliArgsSlice` | 134×48 | `command-line-arguments` |
| `cli-flags-parse` | `CliFlagsParse` | 204×42 | `command-line-flags` |
| `context-cancel-tree` | `ContextCancelTree` | 200×78 | `context` |
| `custom-error-fields` | `CustomErrorFields` | 210×86 | `custom-errors` |
| `env-key-values` | `EnvKeyValues` | 184×44 | `environment-variables` |
| `epoch-offset` | `EpochOffset` | 222×56 | `epoch` |
| `structured-log-entry` | `StructuredLogEntry` | 218×68 | `logging` |
| `mutex-critical` | `MutexCriticalSection` | 250×54 | `mutexes` |
| `number-parse-flow` | `NumberParseFlow` | 220×64 | `number-parsing` |
| `panic-recover-stack` | `PanicRecoverStack` | 192×82 | `panic` |
| `range-runes` | `RangeRunes` | 86×50 | `range-over-built-in-types` |
| `channel-buffering-cap` | `ChannelBuffering` | 200×54 | `channel-buffering` |
| `cli-subcommands` | `CliSubcommands` | 154×92 | `command-line-subcommands` |
| `directory-tree` | `DirectoryTree` | 180×108 | `directories` |
| `embed-directive` | `EmbedDirective` | 218×58 | `embed-directive` |
| `exec-process` | `ExecProcess` | 228×54 | `execing-processes` |
| `exit-status` | `ExitStatus` | 180×54 | `exit` |
| `file-path-parts` | `FilePathParts` | 190×44 | `file-paths` |
| `http-client-roundtrip` | `HttpClientRoundtrip` | 218×48 | `http-client` |
| `http-server-listener` | `HttpServerListener` | 240×60 | `http-server` |
| `line-filter` | `LineFilter` | 270×44 | `line-filters` |
| `non-blocking-select` | `NonBlockingSelect` | 174×64 | `non-blocking-channel-operations` |
| `random-numbers` | `RandomNumbers` | 200×54 | `random-numbers` |
| `range-over-channels` | `RangeOverChannels` | 256×44 | `range-over-channels` |
| `range-iterator` | `RangeIterator` | 180×56 | `range-over-iterators` |
| `rate-limiter` | `RateLimiter` | 202×66 | `rate-limiting` |
| `read-file-buffer` | `ReadFileBuffer` | 186×60 | `reading-files` |
| `recover-flow` | `RecoverFlow` | 210×88 | `recover` |
| `regex-captures` | `RegexCaptures` | 260×54 | `regular-expressions` |
| `select-race` | `SelectRace` | 236×84 | `select` |
| `sha256-digest` | `Sha256Digest` | 250×44 | `sha256-hashes` |
| `signal-channel` | `SignalChannel` | 220×58 | `signals` |
| `sort-comparator` | `SortComparator` | 210×88 | `sorting-by-functions` |
| `spawn-process-pipe` | `SpawnProcessPipe` | 260×58 | `spawning-processes` |
| `stateful-owner` | `StatefulOwner` | 260×88 | `stateful-goroutines` |
| `string-ops` | `StringOps` | 270×52 | `string-functions` |
| `string-rune-bytes` | `StringRuneBytes` | 250×56 | `strings-and-runes` |
| `struct-embedding` | `StructEmbedding` | 240×84 | `struct-embedding` |
| `temp-file-lifecycle` | `TempFileLifecycle` | 260×58 | `temp-files-and-directories` |
| `test-benchmark-flow` | `TestBenchmarkFlow` | 250×84 | `testing-and-benchmarking` |
| `text-template-render` | `TextTemplateRender` | 250×58 | `text-templates` |
| `ticker-stream` | `TickerStream` | 220×58 | `tickers` |
| `time-fields` | `TimeFields` | 260×46 | `time` |
| `time-format-parse` | `TimeFormatParse` | 276×50 | `time-formatting-parsing` |
| `timeout-select` | `TimeoutSelect` | 238×84 | `timeouts` |
| `waitgroup-counter` | `WaitGroupCounter` | 250×86 | `waitgroups` |
| `worker-pool` | `WorkerPool` | 270×88 | `worker-pools` |
| `write-file-buffer` | `WriteFileBuffer` | 250×56 | `writing-files` |
| `xml-roundtrip` | `XmlRoundtrip` | 272×56 | `xml` |

---

## Troubleshooting

**My marks are clipped at the bottom.**
Increase the `h` value in the `Registry` entry.  The canvas adds 14 units of
padding automatically but only around the registered size.

**The figure renders too large / too small.**
The SVG is output at 1.6× the viewBox size.  To make it smaller, reduce `w`
and `h`.  CSS `max-width: 100%` ensures it never overflows on mobile.

**The orange doesn't stand out.**
Make sure you are only using `emphasis: true` on one mark.  If multiple marks
use emphasis, none of them read as special.

**The figure doesn't appear on the page.**
Check that:
1. The slug in `FigureAttachments` exactly matches the directory name under
   `examples/` (case-insensitive, but kebab-case by convention).
2. The figure name in the attachment matches a key in `Figures.Registry`.
3. You ran `dotnet run --project src/dotnetbyexample/` after the change.
