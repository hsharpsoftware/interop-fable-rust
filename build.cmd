call npm i
call fable library.fsx
call webpack
rustc --target asmjs-unknown-emscripten main.rs
rustc --target asmjs-unknown-emscripten lib.rs
copy main.js public
copy lib.js public
