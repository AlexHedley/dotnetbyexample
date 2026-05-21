# Figure Plan

Tracks which example pages have a visual figure banner and which still need one.

A figure is added by:

1. Writing a paint method in `src/dotnetbyexample/Marginalia/Figures.cs`.
2. Registering it in `Figures.Registry` (same file).
3. Adding an entry to `FigureAttachments.Attachments` in `src/dotnetbyexample/Marginalia/FigureAttachments.cs`.

---

## Progress

62 of 84 examples have figures (22 remaining).

| Example | Figure | Status |
|---|---|---|
| `arrays` | `array-index` | ✅ done |
| `atomic-counters` | `atomic-counter-sync` | ✅ done |
| `base64-encoding` | `base64-roundtrip` | ✅ done |
| `channel-buffering` | `channel-buffering-cap` | ✅ done |
| `channel-directions` | `channel-directions` | ✅ done |
| `channel-synchronization` | `channel-sync` | ✅ done |
| `channels` | `channel-buffer` | ✅ done |
| `closing-channels` | `channel-close-range` | ✅ done |
| `closures` | `closure-cell` | ✅ done |
| `command-line-arguments` | `cli-args-slice` | ✅ done |
| `command-line-flags` | `cli-flags-parse` | ✅ done |
| `command-line-subcommands` | `cli-subcommands` | ✅ done |
| `constants` | `const-value` | ✅ done |
| `context` | `context-cancel-tree` | ✅ done |
| `custom-errors` | `custom-error-fields` | ✅ done |
| `defer` | `lifo-defer` | ✅ done |
| `directories` | `directory-tree` | ✅ done |
| `embed-directive` | `embed-directive` | ✅ done |
| `enums` | `enum-values` | ✅ done |
| `environment-variables` | `env-key-values` | ✅ done |
| `epoch` | `epoch-offset` | ✅ done |
| `errors` | `exception-flow` | ✅ done |
| `execing-processes` | `exec-process` | ✅ done |
| `exit` | `exit-status` | ✅ done |
| `file-paths` | `file-path-parts` | ✅ done |
| `for` | `for-loop` | ✅ done |
| `functions` | `function-call` | ✅ done |
| `generics` | `generic-box` | ✅ done |
| `goroutines` | `goroutine-lanes` | ✅ done |
| `hello-world` | `console-writeline` | ✅ done |
| `http-client` | `http-client-roundtrip` | ✅ done |
| `http-server` | `http-server-listener` | ✅ done |
| `if-else` | `if-else-branch` | ✅ done |
| `interfaces` | `interface-contract` | ✅ done |
| `json` | `json-serialize` | ✅ done |
| `line-filters` | `line-filter` | ✅ done |
| `logging` | `structured-log-entry` | ✅ done |
| `maps` | `map-entries` | ✅ done |
| `methods` | `method-slots` | ✅ done |
| `multiple-return-values` | `multi-return` | ✅ done |
| `mutexes` | `mutex-critical` | ✅ done |
| `non-blocking-channel-operations` | `non-blocking-select` | ✅ done |
| `number-parsing` | `number-parse-flow` | ✅ done |
| `panic` | `panic-recover-stack` | ✅ done |
| `pointers` | `ref-alias` | ✅ done |
| `random-numbers` | `random-numbers` | ✅ done |
| `range-over-built-in-types` | `range-runes` | ✅ done |
| `range-over-channels` | `range-over-channels` | ✅ done |
| `range-over-iterators` | `range-iterator` | ✅ done |
| `rate-limiting` | `rate-limiter` | ✅ done |
| `reading-files` | `read-file-buffer` | ✅ done |
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
| `recover` | Stack unwind intercepted by a deferred recover call |
| `regular-expressions` | Pattern matched against input with capture groups highlighted |
| `select` | Select chooses the first ready channel out of several |
| `sha256-hashes` | Input bytes hashed to a fixed-length digest |
| `signals` | OS signal delivered to the process and handled in a goroutine |
| `sorting-by-functions` | Custom comparator function driving element ordering |
| `spawning-processes` | Parent process spawning a child and reading its output |
| `stateful-goroutines` | State owned by one goroutine; channels carry read/write requests |
| `string-functions` | Common string operations (Contains, Split, Replace) visualised |
| `strings-and-runes` | String bytes vs decoded rune code points |
| `struct-embedding` | Embedded struct fields promoted to the outer type |
| `temp-files-and-directories` | Temp file created in the OS temp dir and cleaned up |
| `testing-and-benchmarking` | Test function green-path vs failure path; benchmark loop |
| `text-templates` | Template with action blocks rendered with data to output text |
| `tickers` | Ticker firing repeated events on a fixed interval |
| `time` | Time value with year/month/day/clock fields |
| `time-formatting-parsing` | A time value formatted to a string and parsed back |
| `timeouts` | A channel receive with a timeout via select |
| `waitgroups` | WaitGroup counter counting goroutines; Done decrements; Wait blocks |
| `worker-pools` | Fixed pool of workers consuming from a job channel |
| `writing-files` | In-memory bytes written to a file on disk |
| `xml` | C# object serialised to an XML element and deserialised back |
