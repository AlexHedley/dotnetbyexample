# Figure Plan

Tracks which example pages have a visual figure banner and which still need one.

A figure is added by:

1. Writing a paint method in `src/dotnetbyexample/Marginalia/Figures.cs`.
2. Registering it in `Figures.Registry` (same file).
3. Adding an entry to `FigureAttachments.Attachments` in `src/dotnetbyexample/Marginalia/FigureAttachments.cs`.

---

## Progress

30 of 84 examples have figures (54 remaining).

| Example | Figure | Status |
|---|---|---|
| `arrays` | `array-index` | ✅ done |
| `atomic-counters` | — | ⬜ todo |
| `base64-encoding` | — | ⬜ todo |
| `channel-buffering` | — | ⬜ todo |
| `channel-directions` | — | ⬜ todo |
| `channel-synchronization` | — | ⬜ todo |
| `channels` | `channel-buffer` | ✅ done |
| `closing-channels` | — | ⬜ todo |
| `closures` | `closure-cell` | ✅ done |
| `command-line-arguments` | — | ⬜ todo |
| `command-line-flags` | — | ⬜ todo |
| `command-line-subcommands` | — | ⬜ todo |
| `constants` | `const-value` | ✅ done |
| `context` | — | ⬜ todo |
| `custom-errors` | — | ⬜ todo |
| `defer` | `lifo-defer` | ✅ done |
| `directories` | — | ⬜ todo |
| `embed-directive` | — | ⬜ todo |
| `enums` | `enum-values` | ✅ done |
| `environment-variables` | — | ⬜ todo |
| `epoch` | — | ⬜ todo |
| `errors` | `exception-flow` | ✅ done |
| `execing-processes` | — | ⬜ todo |
| `exit` | — | ⬜ todo |
| `file-paths` | — | ⬜ todo |
| `for` | `for-loop` | ✅ done |
| `functions` | `function-call` | ✅ done |
| `generics` | `generic-box` | ✅ done |
| `goroutines` | `goroutine-lanes` | ✅ done |
| `hello-world` | `console-writeline` | ✅ done |
| `http-client` | — | ⬜ todo |
| `http-server` | — | ⬜ todo |
| `if-else` | `if-else-branch` | ✅ done |
| `interfaces` | `interface-contract` | ✅ done |
| `json` | `json-serialize` | ✅ done |
| `line-filters` | — | ⬜ todo |
| `logging` | — | ⬜ todo |
| `maps` | `map-entries` | ✅ done |
| `methods` | `method-slots` | ✅ done |
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
| `sorting` | `sort-before-after` | ✅ done |
| `sorting-by-functions` | — | ⬜ todo |
| `spawning-processes` | — | ⬜ todo |
| `stateful-goroutines` | — | ⬜ todo |
| `string-formatting` | `format-placeholders` | ✅ done |
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
| `timers` | `timer-tick` | ✅ done |
| `url-parsing` | `url-parts` | ✅ done |
| `values` | `number-lines` | ✅ done |
| `variables` | `variables-bind` | ✅ done |
| `variadic-functions` | `params-array` | ✅ done |
| `waitgroups` | — | ⬜ todo |
| `worker-pools` | — | ⬜ todo |
| `writing-files` | — | ⬜ todo |
| `xml` | — | ⬜ todo |

---

## Suggested next batch

Good candidates for the next set of figures:

| Example | Suggested figure concept |
|---|---|
| `atomic-counters` | A counter value incremented by two concurrent goroutines with a sync marker |
| `base64-encoding` | A byte sequence encoded to a base-64 string and decoded back |
| `channel-directions` | A channel parameter labelled send-only vs receive-only |
| `channel-synchronization` | A goroutine signalling completion to the main goroutine via a channel |
| `closing-channels` | A channel being closed; range terminates automatically |
| `command-line-arguments` | `os.Args` slice with program name at index 0 and arguments following |
| `command-line-flags` | Defined flags with name, default, and parsed value |
| `context` | A context tree with cancellation propagating downward |
| `custom-errors` | A custom error type with extra fields beyond the message string |
| `environment-variables` | OS environment key/value pairs read at startup |
| `epoch` | Unix epoch at time zero and the current timestamp offset from it |
| `logging` | A structured log entry showing level, timestamp, and message fields |
| `mutexes` | A mutex guarding shared state: lock → read/write → unlock |
| `number-parsing` | A string input parsed to int and float with an error path |
| `panic` | Panic unwinds the call stack until a recover catches it |
| `range-over-built-in-types` | Range over a string yielding rune index pairs |
