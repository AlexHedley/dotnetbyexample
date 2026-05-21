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

## Reference — existing figures

| Figure name | Paint method | Canvas (w×h) | Attached to |
|-------------|-------------|-------------|-------------|
| `number-lines` | `NumberLines` | 260 × 78 | `values` |
| `value-types` | `ValueTypes` | 160 × 120 | — |
| `call-stack` | `CallStack` | 200 × 100 | `recursion` |
| `closure-cell` | `ClosureCell` | 240 × 120 | `closures` |
| `variables-bind` | `VariablesBind` | 180 × 44 | `variables` |
| `for-loop` | `ForLoop` | 210 × 46 | `for` |
| `struct-fields` | `StructFields` | 206 × 96 | `structs` |

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
