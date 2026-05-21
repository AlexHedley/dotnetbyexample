# Figure Plan

Tracks which example pages have a visual figure banner and which still need one.

A figure is added by:

1. Writing a paint method in `src/dotnetbyexample/Marginalia/Figures.cs`.
2. Registering it in `Figures.Registry` (same file).
3. Adding an entry to `FigureAttachments.Attachments` in `src/dotnetbyexample/Marginalia/FigureAttachments.cs`.

---

## Progress

14 of 84 examples have figures (70 remaining).

| Example | Figure | Status |
|---|---|---|
| `arrays` | `array-index` | ✅ done |
| `atomic-counters` | — | ⬜ todo |
| `base64-encoding` | — | ⬜ todo |
| `channel-buffering` | — | ⬜ todo |
| `channel-directions` | — | ⬜ todo |
| `channel-synchronization` | — | ⬜ todo |
| `channels` | — | ⬜ todo |
| `closing-channels` | — | ⬜ todo |
| `closures` | `closure-cell` | ✅ done |
| `command-line-arguments` | — | ⬜ todo |
| `command-line-flags` | — | ⬜ todo |
| `command-line-subcommands` | — | ⬜ todo |
| `constants` | — | ⬜ todo |
| `context` | — | ⬜ todo |
| `custom-errors` | — | ⬜ todo |
| `defer` | — | ⬜ todo |
| `directories` | — | ⬜ todo |
| `embed-directive` | — | ⬜ todo |
| `enums` | — | ⬜ todo |
| `environment-variables` | — | ⬜ todo |
| `epoch` | — | ⬜ todo |
| `errors` | — | ⬜ todo |
| `execing-processes` | — | ⬜ todo |
| `exit` | — | ⬜ todo |
| `file-paths` | — | ⬜ todo |
| `for` | `for-loop` | ✅ done |
| `functions` | `function-call` | ✅ done |
| `generics` | — | ⬜ todo |
| `goroutines` | — | ⬜ todo |
| `hello-world` | — | ⬜ todo |
| `http-client` | — | ⬜ todo |
| `http-server` | — | ⬜ todo |
| `if-else` | `if-else-branch` | ✅ done |
| `interfaces` | — | ⬜ todo |
| `json` | — | ⬜ todo |
| `line-filters` | — | ⬜ todo |
| `logging` | — | ⬜ todo |
| `maps` | `map-entries` | ✅ done |
| `methods` | — | ⬜ todo |
| `multiple-return-values` | `multi-return` | ✅ done |
| `mutexes` | — | ⬜ todo |
| `non-blocking-channel-operations` | — | ⬜ todo |
| `number-parsing` | — | ⬜ todo |
| `panic` | — | ⬜ todo |
| `pointers` | `ref-alias` | ✅ done |
| `random-numbers` | — | ⬜ todo |
| `range-over-built-in-types` | — | ⬜ todo |
| `range-over-channels` | — | ⬜ todo |
| `range-over-iterators` | — | ⬜ todo |
| `rate-limiting` | — | ⬜ todo |
| `reading-files` | — | ⬜ todo |
| `recover` | — | ⬜ todo |
| `recursion` | `call-stack` | ✅ done |
| `regular-expressions` | — | ⬜ todo |
| `select` | — | ⬜ todo |
| `sha256-hashes` | — | ⬜ todo |
| `signals` | — | ⬜ todo |
| `slices` | `slice-window` | ✅ done |
| `sorting` | — | ⬜ todo |
| `sorting-by-functions` | — | ⬜ todo |
| `spawning-processes` | — | ⬜ todo |
| `stateful-goroutines` | — | ⬜ todo |
| `string-formatting` | — | ⬜ todo |
| `string-functions` | — | ⬜ todo |
| `strings-and-runes` | — | ⬜ todo |
| `struct-embedding` | — | ⬜ todo |
| `structs` | `struct-fields` | ✅ done |
| `switch` | `switch-cases` | ✅ done |
| `temp-files-and-directories` | — | ⬜ todo |
| `testing-and-benchmarking` | — | ⬜ todo |
| `text-templates` | — | ⬜ todo |
| `tickers` | — | ⬜ todo |
| `time` | — | ⬜ todo |
| `time-formatting-parsing` | — | ⬜ todo |
| `timeouts` | — | ⬜ todo |
| `timers` | — | ⬜ todo |
| `url-parsing` | — | ⬜ todo |
| `values` | `number-lines` | ✅ done |
| `variables` | `variables-bind` | ✅ done |
| `variadic-functions` | — | ⬜ todo |
| `waitgroups` | — | ⬜ todo |
| `worker-pools` | — | ⬜ todo |
| `writing-files` | — | ⬜ todo |
| `xml` | — | ⬜ todo |

---

## Suggested next batch

Good candidates for the next set of figures (concepts that translate well to simple diagrams):

| Example | Suggested figure concept |
|---|---|
| `hello-world` | A `Console.WriteLine` call box emitting a string to stdout |
| `constants` | A `const` value box labelled as compile-time / no memory slot |
| `enums` | A fixed set of named integer values shown as a labelled row |
| `methods` | A `Rect` struct box with `Area()` and `Perim()` method slots |
| `interfaces` | Two concrete types (`Rect`, `Circle`) implementing one interface contract |
| `errors` | A try/catch flow: normal path vs exception path |
| `generics` | A single generic container box `T` with two concrete instantiations |
| `sorting` | A list before and after sorting with directional arrows |
| `variadic-functions` | A call with N arguments collapsing into a single `params` array |
| `defer` | A LIFO call stack of deferred calls unwinding after the function returns |
| `goroutines` | Two concurrent goroutine execution lanes running in parallel |
| `channels` | A buffered channel with producer and consumer arrows |
| `timers` | A timeline with a single tick event at the deadline |
| `url-parsing` | A URL split into scheme, host, path, and query components |
| `json` | A C# object being serialized to a JSON string and back |
| `string-formatting` | A format string with placeholders pointing to their argument values |
