// The main goal of this task is to parse two arrays of integers, sort them and
// then calculate a custom metric called distance between twose numbers.

use std::{collections::HashMap, fs::read_to_string};

const FILENAME: &str = "/home/padjal/dev/avent-of-code/2024/day_1/src/input.txt";

fn main() {
    let (mut a, mut b) = read_lines(FILENAME);
    a.sort();
    b.sort();

    let mut result = 0;

    for i in 0..a.len() {
        result += distance(a[i], b[i]);
    }

    let similarity: i32 = calculate_similarity(a, b);

    println!("{}", result);
    println!("Similarity: {similarity}");
}

fn read_lines(filename: &str) -> (Vec<i32>, Vec<i32>) {
    let mut a: Vec<i32> = Vec::new();
    let mut b: Vec<i32> = Vec::new();

    read_to_string(filename)
        .unwrap() // panic on possible file-reading errors
        .lines() // split the string into an iterator of string slices
        .for_each(|line| {
            let (number_1, number_2) = parse_line(line);
            a.push(number_1);
            b.push(number_2);
        }); // make each slice into a string

    (a, b)
}

fn parse_line(line: &str) -> (i32, i32) {
    let mut parts = line.split_whitespace();
    let a = parts.next().unwrap().parse().unwrap();
    let b = parts.next().unwrap().parse().unwrap();
    (a, b)
}

fn distance(a: i32, b: i32) -> i32 {
    (a - b).abs()
}

fn calculate_similarity(a: Vec<i32>, b: Vec<i32>) -> i32 {
    let mut result = 0;
    let mut occurence: HashMap<i32, i32> = HashMap::new();

    // Count the occurence of each number in the second array
    for i in 0..a.len() {
        let number = b[i];

        if occurence.contains_key(&number) {
            occurence.insert(number, occurence.get(&number).unwrap() + 1);
        } else {
            occurence.insert(number, 1);
        }
    }

    for i in 0..a.len() {
        let multiplyer = match occurence.get(&a[i]) {
            Some(&value) => value,
            None => 0,
        };

        result += a[i] * multiplyer;
    }

    result
}
