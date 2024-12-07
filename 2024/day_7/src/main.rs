use std::collections::LinkedList;
use std::fs::{read, read_to_string};
use std::str;

struct Calculation {
    result: i64,
    numbers: Vec<usize>,
}

fn main() {
    let input = parse_input("./src/input.txt");

    let result = calculate(input);

    let input = parse_input("./src/input.txt");
    let result2 = calculate2(input);

    println!("Result: {}", result);
    println!("Result2: {}", result2);
}

#[test]
fn day1() {
    // Asign
    let input = parse_input("./src/example_input.txt");

    // Act
    let result = calculate(input);

    // Assert
    assert_eq!(result, 3749);
}

#[test]
fn day2() {
    // Asign
    let input = parse_input("./src/example_input.txt");

    // Act
    let result = calculate2(input);

    // Assert
    assert_eq!(result, 11387);
}

fn parse_input(file_name: &str) -> Vec<Calculation> {
    let input = read_to_string(file_name).expect("Could not read file");
    let mut calculations: Vec<Calculation> = Vec::new();

    let calculation_two: Vec<Calculation> = input
        .lines()
        .map(|line| {
            let split: Vec<&str> = line.split(':').collect();
            let nums = split[1].trim().split(' ');

            let calc = Calculation {
                result: split[0].parse().unwrap(),
                numbers: nums.map(|n| n.parse().unwrap()).collect(),
            };

            calc
        })
        .collect();

    calculation_two
}

fn calculate(input: Vec<Calculation>) -> i64 {
    let mut result = 0;

    for calc in input {
        if equation_is_possible(&calc, calc.numbers[0] as i64, 1) {
            result += calc.result;
        }
    }

    result
}

fn calculate2(input: Vec<Calculation>) -> i64 {
    let mut result = 0;

    for calc in input {
        if equation_is_possible2(&calc, calc.numbers[0] as i64, 1) {
            result += calc.result;
        }
    }

    result
}

// Recursive function to check if the equation is possible
fn equation_is_possible(equation: &Calculation, current: i64, index: usize) -> bool {
    //
    if current > equation.result {
        return false;
    }

    if index == equation.numbers.len() {
        return current == equation.result;
    }

    return equation_is_possible(
        equation,
        current + equation.numbers[index] as i64,
        index + 1,
    ) || equation_is_possible(
        equation,
        current * equation.numbers[index] as i64,
        index + 1,
    );
}

// Recursive function to check if the equation is possible
fn equation_is_possible2(equation: &Calculation, current: i64, index: usize) -> bool {
    //
    if current > equation.result {
        return false;
    }

    if index == equation.numbers.len() {
        return current == equation.result;
    }

    return equation_is_possible2(
        equation,
        current + equation.numbers[index] as i64,
        index + 1,
    ) || equation_is_possible2(
        equation,
        current * equation.numbers[index] as i64,
        index + 1,
    ) || equation_is_possible2(
        equation,
        concat(current, equation.numbers[index] as i64),
        index + 1,
    );
}

fn concat(num1: i64, num2: i64) -> i64 {
    let mut pow = 1;

    while num2 % i64::pow(10, pow) != num2 {
        pow += 1;
    }

    num1 * i64::pow(10, pow) + num2
}
