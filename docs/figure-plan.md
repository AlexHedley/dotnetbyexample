# Figure Plan

Tracks which example pages have a visual figure banner and which still need one.

A figure is added by:

1. Writing a paint method in `src/dotnetbyexample/Marginalia/Figures.cs`.
2. Registering it in `Figures.Registry` (same file).
3. Adding an entry to `FigureAttachments.Attachments` in `src/dotnetbyexample/Marginalia/FigureAttachments.cs`.

---

## Progress

46 of 84 examples have figures (38 remaining).

| Example | Figure | Status |
|---|---|---|
| `arrays` | `array-index` | ✅ done |
| `atomic-counters` | `atomic-counter-sync` | ✅ done |
| `base64-encoding` | `base64-roundtrip` | ✅ done |
| `channel-buffering` | — | ⬜ todo |
| `channel-directions` | `channel-directions` | ✅ done |
| `channel-synchronization` | `channel-sync` | ✅ done |
| `channels` | `channel-buffer` | ✅ done |
| `closing-channels` | `channel-close-range` | ✅ done |
| `closures` | `closure-cell` | ✅ done |
| `command-line-arguments` | `cli-args-slice` | ✅ done |
| `command-line-flags` | `cli-flags-parse` | ✅ done |
| `command-line-subcommands` | — | ⬜ todo |
| `constants` | `const-value` | ✅ done |
| `context` | `context-cancel-tree` | ✅ done |
| `custom-errors` | `custom-error-fields` | ✅ done |
| `defer` | `lifo-defer` | ✅ done |
| `directories` | — | ⬜ todo |
| `embed-directive` | — | ⬜ todo |
| `enums` | `enum-values` | ✅ done |
| `environment-variables` | `env-key-values` | ✅ done |
| `epoch` | `epoch-offset` | ✅ done |
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
| `logging` | `structured-log-entry` | ✅ done |
| `maps` | `map-entries` | ✅ done |
| `methods` | `method-slots` | ✅ done |
| `multiple-return-values` | `multi-return` | ✅ done |
| `mutexes` | `mutex-critical` | ✅ done |
| `non-blocking-channel-operations` | — | ⬜ todo |
| `number-parsing` | `number-parse-flow` | ✅ done |
| `panic` | `panic-recover-stack` | ✅ done |
| `pointers` | `ref-alias` | ✅ done |
| `random-numbers` | — | ⬜ todo |
| `range-over-built-in-types` | `range-runes` | ✅ done |
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
| `channel-buffering` | A buffered channel with capacity markers filling and draining |
| `command-line-subcommands` | A root command dispatching to named subcommands |
| `directories` | A directory tree with parent and child folder nodes |
| `embed-directive` | A source file embedding static assets into a binary |
| `execing-processes` | Current process replaced by a new executable image |
| `exit` | Program control flow terminating with an exit status code |
| `file-paths` | A path split into directory, filename, and extension components |
| `http-client` | A request/response roundtrip between client and server |
| `http-server` | Listener accepting requests and returning responses |
| `line-filters` | stdin lines transformed and emitted to stdout |
| `non-blocking-channel-operations` | Select with default path for immediate fallback |
| `random-numbers` | RNG source producing different values each call |
| `range-over-channels` | Consumer ranging over values until channel close |
| `range-over-iterators` | Iterator yielding sequence values into a range loop |
| `rate-limiting` | Token/tick stream gating request processing |
| `reading-files` | File bytes read from disk into memory buffer |
