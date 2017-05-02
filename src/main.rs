#![feature(libc)]
extern crate libc;

use std::ffi::CString;
use std::ffi::CStr;

/// Safe rust wrapper for emscripten_run_script_int (basically, JS eval()).
pub fn eval(x: &str) -> i32 {
    let x = CString::new(x).unwrap();
    let ptr = x.as_ptr();
    unsafe { ffi::emscripten_run_script_int(ptr) }
}

pub fn eval_s(x: &str) -> String {
    let x = CString::new(x).unwrap();
    let ptr = x.as_ptr();
    unsafe { CStr::from_ptr(ffi::emscripten_run_script_string(ptr)).to_string_lossy().into_owned() }
}

// This is mostly standard Rust-C FFI stuff.
mod ffi {
    use libc::*;

    extern "C" {
        // This extern is built in by Emscripten.
        pub fn emscripten_run_script_int(x: *const c_char) -> c_int;
        pub fn emscripten_run_script_string(x: *const c_char) -> *const c_char;
    }
}

extern crate serde;
extern crate serde_json;

#[macro_use]
extern crate serde_derive;

#[derive(Serialize, Deserialize)]
#[derive(Debug)]
struct Person {
    name: String,
    age: u8,
    phones: Vec<String>,
}

fn typed_example(data : &str) -> Person {
    let v: Person = serde_json::from_str(data).unwrap();
    v
}

fn main() {
    println!("Rust code in main() started...");
    println!("Fable returns {}", eval(&format!("FableLib.fableFunc({})", 1)));
    let result = eval_s("FableLib.fableFuncS()");
    println!("Fable also returns {}", result);    
    let person = typed_example(&result);
    println!("which is {:?} named {}",person, person.name);
    println!("... and we are done!");
}
