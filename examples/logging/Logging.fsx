// Logging in F#.

// Basic console logging.
printfn "standard logger"

// Timestamped log messages.
let log level msg =
    printfn "%s [%s] %s" (System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) level msg

log "INFO" "log message"
log "INFO" "log with fields: key=val"
log "DEBUG" "debug message"
log "WARN" "warning message"
log "ERROR" "error message"
