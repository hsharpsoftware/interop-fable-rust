rem call npm i
call fable
call webpack
cargo build --target=asmjs-unknown-emscripten
copy .\target\asmjs-unknown-emscripten\debug\interop-fable-rust.js public
