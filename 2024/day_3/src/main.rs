use regex::Regex;
use std::fs::read_to_string;

const FILE_PATH: &str = "./src/input.txt";

fn main() {
    let input = read_to_string(FILE_PATH).expect("Failed to read file");

    let multiply_regex =
        Regex::new(r"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)").expect("Failed to create regex");

    let mut result = 0;

    let mut is_enabled = true;

    for mat in multiply_regex.find_iter(&input) {
        match mat.as_str() {
            "do()" => {
                is_enabled = true;
                continue;
            }
            "don't()" => {
                is_enabled = false;
                continue;
            }
            _ => {
                if !is_enabled {
                    continue;
                }
            }
        }

        // Parse the string to get the numbers
        let mut nums = mat.as_str()[4..mat.as_str().len() - 1].split(",");
        let a: i32 = nums
            .next()
            .unwrap()
            .parse()
            .expect("Failed to parse number");
        let b: i32 = nums
            .next()
            .unwrap()
            .parse()
            .expect("Failed to parse number");

        result += a * b;
        println!("Multiplying: {} * {} = {}", a, b, a * b);
        println!("Result: {}", result);
    }

    println!("Result: {result}");
}
