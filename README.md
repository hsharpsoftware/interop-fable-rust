# interop-fable-rust
JavaScript Interop between F# (Fable) and Rust (through Emscripten)

For now only calling [F#/Fable](http://fable.io/) functions from [Rust](https://www.rust-lang.org/en-US/) through [asmjs-unknown-emscripten](http://www.hellorust.com/emscripten/) is working. The other way around [calling Rust function from Fable](https://github.com/hsharpsoftware/interop-fable-rust/tree/callback) is not working yet, probably because of using two modules at the same page.

Some basic JSON object exchange also works see below.

![Ex](json.PNG "JSON data exchange" )
