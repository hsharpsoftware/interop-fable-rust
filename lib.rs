#![feature(link_args)]

#[link_args = "-s EXPORTED_FUNCTIONS=['_hello_world']"]
extern {}

#[no_mangle]
pub fn hello_world() -> isize {
    println!("Hello World!");

    41 + 1
}

fn main() {
    /* Intentionally left blank */
}
