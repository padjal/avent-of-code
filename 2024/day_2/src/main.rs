use core::error;
use std::env;
use std::fs::read_to_string;

fn main() {
    let input = parse_input("./src/input.txt");

    let safe_reports = count_safe_reports(&input);

    println!("Safe reports: {}", safe_reports);

    println!(
        "Safe reports with tolerance: {}",
        count_safe_reports_with_tolerance(&input)
    );
}

fn parse_input(file_path: &str) -> Vec<Vec<i32>> {
    let input = read_to_string(file_path).expect("Failed to read file");

    let mut result = Vec::new();
    for line in input.lines() {
        let mut row = Vec::new();
        for number in line.split_whitespace() {
            row.push(number.parse().unwrap());
        }
        result.push(row);
    }
    result
}

/// Count the number of safe reports
fn count_safe_reports(input: &Vec<Vec<i32>>) -> i32 {
    let mut count = 0;
    for row in input {
        let is_safe: bool = is_safe(&row);

        if is_safe {
            count += 1;
        }
    }
    count
}

/// Count the number of safe reports with tolerance
fn count_safe_reports_with_tolerance(input: &Vec<Vec<i32>>) -> i32 {
    let mut count = 0;
    for row in input {
        let is_sequnce_safe: bool = is_safe(&row);

        if is_sequnce_safe {
            count += 1;
        } else {
            // Try the same, but this time remove an element from the sequence
            for i in 0..row.len() {
                let mut sequence = row.clone();
                sequence.remove(i);

                if is_safe(&sequence) {
                    count += 1;
                    break;
                }
            }
        }
    }
    count
}

fn is_safe(sequence: &Vec<i32>) -> bool {
    let is_increasing = sequence[0] < sequence[1];
    let mut is_safe: bool = true;

    for i in 1..sequence.len() {
        // Check if the difference between the current and previous number is less than 1 or greater than 3
        if distance(sequence[i], sequence[i - 1]) > 3 || distance(sequence[i], sequence[i - 1]) < 1
        {
            is_safe = false;
            break;
        }

        if is_increasing && sequence[i - 1] >= sequence[i] {
            is_safe = false;
        } else if !is_increasing && sequence[i - 1] < sequence[i] {
            is_safe = false;
            break;
        }
    }

    is_safe
}

fn distance(a: i32, b: i32) -> i32 {
    (a - b).abs()
}
