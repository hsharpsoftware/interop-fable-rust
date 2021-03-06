#![feature(libc)]
#![feature(link_args)]
#[link_args = "-s EXPORTED_FUNCTIONS=['_app','_callback','_process_state']"]
extern {}

extern crate libc;

use std::ffi::CString;
use std::ffi::CStr;
use libc::c_char;
use std::mem;

const  STATE_INITIAL: i32 = 0;
const  STATE_LOADING: i32 = 1;
const  STATE_LOADED: i32 = 2;
const  STATE_CHANGED: i32 = 3;

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

#[derive(Serialize, Deserialize)]
#[derive(Debug)]
struct State {
    state : i32,
    person : Option<Person>
}

fn render_ui() -> String {
    eval_s("FableLib.render()")
}

fn convert_from_js( data : *mut c_char ) -> String {
    unsafe {
        let s = CString::from_raw(data);
        let s = s.into_string().unwrap();    
        let result = s.clone();
        mem::forget(s);
        result
    }    
}

#[no_mangle]
pub fn process_state( data : *mut c_char ) -> *mut c_char {
    let received = convert_from_js(data);
    println!( "Processing...{:?}", received );
    let state : State = serde_json::from_str(&received).unwrap();
    println!( "Got {:?}", state );
    let result = 
        if state.state == STATE_INITIAL { 
            println!("Loading state from Fable...");    
            State { state : STATE_LOADING, .. state } 
        } else if state.state == STATE_CHANGED {
            println!("Processing changed state from Fable..."); 
            let person = state.person.unwrap();
            State { state : STATE_LOADED, person : Some(Person { name: (person.name + " (Changed!)").to_string(), .. person }), .. state } 
        }
        else { state };
    println!( "Will return {:?}", result );
    CString::new(json!(result).to_string()).unwrap().into_raw()
}

#[no_mangle]
pub fn app() {
    println!("Rust code in main() started...");
    render_ui();
    println!("... and we are done!");
}

fn main() {
}