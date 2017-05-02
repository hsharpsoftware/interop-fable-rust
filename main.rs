use std::os::raw::c_char;
extern {
    pub fn emscripten_asm_const(s: *const c_char);
}

extern crate lib;

fn main() {
    println!("Code started...");
    let code : &'static [u8] = b"FableLib.fableFunc();\0";
    unsafe {
        emscripten_asm_const(&code[0] as *const c_char);
    }
    println!("... and we are done!");
}
