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
#[macro_use]
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

fn load_person() -> Option<String> {
    Some(eval_s("FableLib.fableFuncS()"))
}

fn parse_person(data : &str) -> Person {
    let v: Person = serde_json::from_str(data).unwrap();
    v
}

fn get_sent_person_age( person : Person ) -> u8 {
    eval(&format!("FableLib.fableFunc({})", json!(person).to_string())) as u8
}

fn main() {
    println!("Rust code in main() started...");
    println!("Loading person from Fable...");
    let person =  load_person().map( |p| parse_person(&p) ).unwrap();
    println!("loaded {:?}; the person'name is {}",person, person.name);
    println!("Fable reports the age to be {}", get_sent_person_age(person) );
    println!("... and we are done!");
}
