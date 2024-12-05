use std::fs::read_to_string;

const FILE_PATH: &str = "./src/input.txt";

struct Rule {
    first: i32,
    second: i32,
}

fn main() {
    let (rules, updates) = parse_input(FILE_PATH);

    // Print rules
    // println!("Rules:");
    // for rule in rules.iter() {
    //     println!("Rule: {} {}", rule.first, rule.second);
    // }

    // Print updates
    // println!("Updates:");
    // for update in updates.iter() {
    //     for number in update.iter() {
    //         print!("{} ", number);
    //     }
    //     println!();
    // }

    let mut result: i32 = 0;
    let mut incorrect_updates: Vec<Vec<i32>> = Vec::new();

    for i in 0..updates.len() {
        let mut is_valid = true;

        // For all updates, check all rules
        for j in 0..rules.len() {
            let index_of_first = updates[i].iter().position(|&x| x == rules[j].first);
            let index_of_second = updates[i].iter().position(|&x| x == rules[j].second);

            if index_of_first.is_none() || index_of_second.is_none() {
                continue;
            }

            if index_of_first >= index_of_second {
                is_valid = false;
                break;
            }
        }

        if is_valid {
            // println!("Update {} is valid", i);
            // Take middle number
            result += updates[i][updates[i].len() / 2]
        } else {
            incorrect_updates.push(updates[i].clone());
        }

        result += updates[i][updates[i].len() / 2]
    }

    println!("Result: {}", result);

    println!("Incorrect updates: {}", incorrect_updates.len());

    let mut incorrect_updates_result = 0;

    // Fix incorrect updates
    for i in 0..incorrect_updates.len() {
        // For all updates, check all rules
        let mut j = 0;
        while j < rules.len() - 1 {
            let index_of_first = incorrect_updates[i]
                .iter()
                .position(|&x| x == rules[j].first);
            let index_of_second = incorrect_updates[i]
                .iter()
                .position(|&x| x == rules[j].second);

            if index_of_first.is_none() || index_of_second.is_none() {
                j += 1;
                continue;
            }

            if index_of_first >= index_of_second {
                if incorrect_updates[i][index_of_first.unwrap()]
                    == incorrect_updates[i][index_of_second.unwrap()]
                {
                    j += 1;
                    continue;
                }

                // println!("Before fix  : {:?}", incorrect_updates[i]);

                // Switch the two numbers
                let temp = incorrect_updates[i][index_of_first.unwrap()];

                // println!(
                //     "Switching {} and {}",
                //     incorrect_updates[i][index_of_first.unwrap()],
                //     incorrect_updates[i][index_of_second.unwrap()]
                // );

                incorrect_updates[i][index_of_first.unwrap()] =
                    incorrect_updates[i][index_of_second.unwrap()];
                incorrect_updates[i][index_of_second.unwrap()] = temp;

                // println!("Fixed update: {:?}", incorrect_updates[i]);

                j = 0;
                continue;
            }

            j += 1;
        }

        incorrect_updates_result += incorrect_updates[i][incorrect_updates[i].len() / 2]
    }

    println!("Incorrect upadates result: {}", incorrect_updates_result);
}

// Parses the input file and returns a tuple of rules and updates
fn parse_input(file_path: &str) -> (Vec<Rule>, Vec<Vec<i32>>) {
    let content = read_to_string(file_path).expect("Failed to read the file");
    let mut rules = Vec::new();
    let mut updates = Vec::new();

    let mut have_rules_finished = false;

    for line in content.lines() {
        // Check if line is empty
        if line.is_empty() {
            have_rules_finished = true;
            continue;
        }

        if !have_rules_finished {
            let rule: Vec<&str> = line.split("|").collect();
            let first: i32 = rule[0].parse().unwrap();
            let second: i32 = rule[1].parse().unwrap();

            rules.push(Rule { first, second });
        } else {
            let numbers = line.split(",").map(|x| x.parse().unwrap()).collect();
            updates.push(numbers);
        }
    }

    (rules, updates)
}
