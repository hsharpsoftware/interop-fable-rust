call npm i
call fable library.fsx
call webpack
rustc --target asmjs-unknown-emscripten main.rs
copy main.js public
