use std::fs::read_to_string;

fn main() {
    const FILE_PATH: &str = "./src/input.txt";
    let mut result = 0;

    // Parse input into a matrix
    let input = parse_input(FILE_PATH);

    //Check input
    // println!("Input:");
    // for row in &input {
    //     for char in row {
    //         print!("{}", char);
    //     }
    //     println!();
    // }

    // Count occurence of XMAS in rows
    let rows_hits = rows(&input);
    println!("Rows hits: {}", rows_hits);
    result += rows_hits;

    // Count occurence of XMAS in columns
    let column_hits = columns(&input);
    println!("Columns hits: {}", column_hits);
    result += column_hits;

    // Count occurence of XMAS in diagonals
    let diagonal_hits = diagonals(&input);
    println!("Diagonals hits: {}", diagonal_hits);
    result += diagonal_hits;

    // Count occurence of XMAS in reverse diagonals
    let input_rotated = rotate_matrix(&input);
    let diagonal_hits_rotated = diagonals(&input_rotated);
    println!("Diagonals hits rotated: {}", diagonal_hits_rotated);
    result += diagonal_hits_rotated;

    println!("Result: {}", result);

    // Count occurence of MAS
    let mas_occurences = find_mas_occurences(&input);
    println!("Mas occurences: {mas_occurences}");
}

fn parse_input(file_path: &str) -> Vec<Vec<char>> {
    let input = read_to_string(file_path).expect("Failed to read file");

    let mut result = Vec::new();
    for line in input.lines() {
        let mut row = Vec::new();
        for char in line.chars() {
            row.push(char);
        }
        result.push(row);
    }
    result
}

// Count the number of occurences of XMAS in rows from left to right and from right to left
fn rows(input: &Vec<Vec<char>>) -> i32 {
    let mut result = 0;
    for row in input {
        // Count from left to right
        for i in 0..row.len() - 3 {
            if row[i] == 'X' && row[i + 1] == 'M' && row[i + 2] == 'A' && row[i + 3] == 'S' {
                result += 1;
            }
        }

        // Count from right to left
        for i in 0..row.len() - 3 {
            if row[i] == 'S' && row[i + 1] == 'A' && row[i + 2] == 'M' && row[i + 3] == 'X' {
                result += 1;
            }
        }
    }
    result
}

// Count the number of occurences of XMAS in columns from top to bottom and from bottom to top
fn columns(input: &Vec<Vec<char>>) -> i32 {
    let mut result = 0;
    for i in 0..input[0].len() {
        // Count from top to bottom
        for j in 0..input.len() - 3 {
            if input[j][i] == 'X'
                && input[j + 1][i] == 'M'
                && input[j + 2][i] == 'A'
                && input[j + 3][i] == 'S'
            {
                result += 1;
            }
        }

        // Count from bottom to top
        for j in 0..input.len() - 3 {
            if input[j][i] == 'S'
                && input[j + 1][i] == 'A'
                && input[j + 2][i] == 'M'
                && input[j + 3][i] == 'X'
            {
                result += 1;
            }
        }
    }
    result
}

// Count the number of occurences of XMAS in diagonals from top left to bottom right and from bottom left to top right
fn diagonals(input: &Vec<Vec<char>>) -> i32 {
    let mut result = 0;
    let rows = input.len();
    let columns = input[0].len();
    let number_of_diagonals = rows + columns - 1;
    let mut diagonals: Vec<Vec<char>> = Vec::with_capacity(number_of_diagonals);

    // Populate
    for i in 0..number_of_diagonals {
        diagonals.push(Vec::new());
    }

    for i in 0..rows {
        for j in 0..columns {
            diagonals[i + j].push(input[i][j]);
        }
    }

    // println!("Diagonals:");
    // for i in 0..diagonals.len() {
    //     if diagonals[i].len() < 4 {
    //         continue;
    //     }

    //     print!("Diagonal {}: ", i);

    //     for j in 0..diagonals[i].len() {
    //         print!("{}", diagonals[i][j]);
    //     }
    //     println!();
    // }

    // Check for XMAS
    for i in 0..diagonals.len() {
        if (diagonals[i].len() < 4) {
            continue;
        }

        for j in 0..diagonals[i].len() - 3 {
            if diagonals[i][j] == 'X'
                && diagonals[i][j + 1] == 'M'
                && diagonals[i][j + 2] == 'A'
                && diagonals[i][j + 3] == 'S'
            {
                result += 1;

                // println!("Found XMAS in diagonal {}", i);
            }
        }

        for j in 0..diagonals[i].len() - 3 {
            if diagonals[i][j] == 'S'
                && diagonals[i][j + 1] == 'A'
                && diagonals[i][j + 2] == 'M'
                && diagonals[i][j + 3] == 'X'
            {
                result += 1;

                // println!("Found reverse XMAS in diagonal {}", i);
            }
        }
    }

    result
}

fn rotate_matrix(input: &Vec<Vec<char>>) -> Vec<Vec<char>> {
    let rows = input.len();
    let columns = input[0].len();

    println!("Initial rows: {rows}");
    println!("Initial columns: {columns}");

    let mut result = Vec::with_capacity(columns);

    // Populate
    for i in 0..columns {
        result.push(Vec::with_capacity(rows));
    }

    for i in 0..columns {
        for j in 0..rows {
            result[i].push(' ');
        }
    }

    for i in 0..rows {
        for j in 0..columns {
            result[rows - j - 1][i] = input[i][j];
        }
    }

    // Print result
    // println!("Reversed matrix");
    // for i in 0..columns {
    //     for j in 0..rows {
    //         print!("{}", result[i][j]);
    //     }
    //     println!();
    // }

    result
}

fn find_mas_occurences(input: &Vec<Vec<char>>) -> i32 {
    let mut result = 0;

    // Traverse the matrix and find all As
    for i in 1..(input.len() - 1) {
        for j in 1..(input[i].len() - 1) {
            if input[i][j] != 'A' {
                continue;
            }

            let mut flag = false;

            // Find if there is an MAS
            if (input[i - 1][j - 1] == 'M' && input[i + 1][j + 1] == 'S'
                || input[i - 1][j - 1] == 'S' && input[i + 1][j + 1] == 'M')
                && (input[i - 1][j + 1] == 'M' && input[i + 1][j - 1] == 'S'
                    || input[i - 1][j + 1] == 'S' && input[i + 1][j - 1] == 'M')
            {
                result += 1;
            }
        }
    }

    result
}
