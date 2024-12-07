use std::collections::{HashMap, HashSet};

enum Direction {
    Up,
    Down,
    Left,
    Right,
}

struct GuardPosition {
    x: usize,
    y: usize,
    direction: Direction,
}

fn main() {
    let mut input = parse_input();
    let rows = input.len();
    let columns = input[0].len();

    let mut guard_position: GuardPosition = GuardPosition {
        x: 0,
        y: 0,
        direction: Direction::Up,
    };

    guard_position = find_guard(&input);

    let mut unique_positions = HashSet::new();
    let mut turns = HashSet::new();

    loop {
        unique_positions.insert((guard_position.x, guard_position.y));

        match guard_position.direction {
            Direction::Up => {
                if guard_position.x == 0 {
                    break;
                }

                if input[guard_position.x - 1][guard_position.y] == '#' {
                    guard_position.direction = Direction::Right;
                    turns.insert((guard_position.x, guard_position.y));
                    continue;
                }

                guard_position.x -= 1;
            }
            Direction::Down => {
                if guard_position.x == rows - 1 {
                    break;
                }

                if input[guard_position.x + 1][guard_position.y] == '#' {
                    guard_position.direction = Direction::Left;
                    turns.insert((guard_position.x, guard_position.y));
                    continue;
                }

                guard_position.x += 1;
            }
            Direction::Left => {
                if guard_position.y == 0 {
                    break;
                }

                if input[guard_position.x][guard_position.y - 1] == '#' {
                    guard_position.direction = Direction::Up;
                    turns.insert((guard_position.x, guard_position.y));
                    continue;
                }

                guard_position.y -= 1;
            }
            Direction::Right => {
                if guard_position.y == columns - 1 {
                    break;
                }

                if input[guard_position.x][guard_position.y + 1] == '#' {
                    guard_position.direction = Direction::Down;
                    turns.insert((guard_position.x, guard_position.y));
                    continue;
                }

                guard_position.y += 1;
            }
        }
    }

    let mut loops = 0;
    input = parse_input();
    guard_position = find_guard(&input);

    unique_positions.remove(&(guard_position.x, guard_position.y));

    for position in unique_positions.iter() {
        input[position.0][position.1] = '#';

        if is_trapped(&input) {
            loops += 1;
        }

        input[position.0][position.1] = '.';
    }

    println!("Number of loops: {}", loops);

    println!("Number of unique positions: {}", unique_positions.len());
}

fn parse_input() -> Vec<Vec<char>> {
    let input = std::fs::read_to_string("./src/input.txt").unwrap();
    input.lines().map(|line| line.chars().collect()).collect()
}

fn insert_or_increase(turns: &mut HashMap<(usize, usize), usize>, turn: &(usize, usize)) {
    if turns.contains_key(&turn) {
        *turns.get_mut(&turn).unwrap() += 1;
    } else {
        turns.insert(*turn, 1);
    }
}

fn is_trapped(input: &Vec<Vec<char>>) -> bool {
    let mut turns: HashMap<(usize, usize), usize> = HashMap::new();
    let mut guard_position = find_guard(input);

    loop {
        if turns.contains_key(&(guard_position.x, guard_position.y))
            && turns[&(guard_position.x, guard_position.y)] > 4
        {
            return true;
        }

        match guard_position.direction {
            Direction::Up => {
                if guard_position.x == 0 {
                    break;
                }

                if input[guard_position.x - 1][guard_position.y] == '#' {
                    insert_or_increase(&mut turns, &(guard_position.x, guard_position.y));
                    guard_position.direction = Direction::Right;
                    continue;
                }

                guard_position.x -= 1;
            }
            Direction::Down => {
                if guard_position.x == input.len() - 1 {
                    break;
                }

                if input[guard_position.x + 1][guard_position.y] == '#' {
                    insert_or_increase(&mut turns, &(guard_position.x, guard_position.y));
                    guard_position.direction = Direction::Left;
                    continue;
                }

                guard_position.x += 1;
            }
            Direction::Left => {
                if guard_position.y == 0 {
                    break;
                }

                if input[guard_position.x][guard_position.y - 1] == '#' {
                    insert_or_increase(&mut turns, &(guard_position.x, guard_position.y));
                    guard_position.direction = Direction::Up;
                    continue;
                }

                guard_position.y -= 1;
            }
            Direction::Right => {
                if guard_position.y == input[0].len() - 1 {
                    break;
                }

                if input[guard_position.x][guard_position.y + 1] == '#' {
                    insert_or_increase(&mut turns, &(guard_position.x, guard_position.y));
                    guard_position.direction = Direction::Down;
                    continue;
                }

                guard_position.y += 1;
            }
        }
    }

    false
}

#[deprecated(note = "Initial approach, but didn't work. is_trapped() should be used instead.")]
fn is_guard_trapped(input: &Vec<Vec<char>>, turns: &HashSet<(usize, usize)>) -> bool {
    let rows = input.len();
    let columns = input[0].len();
    let mut occurences = HashMap::new();

    let mut is_in_map = true;
    let mut guard_position = find_guard(input);

    while is_in_map {
        match guard_position.direction {
            Direction::Up => {
                if guard_position.x == 0 {
                    is_in_map = false;
                    break;
                }

                if input[guard_position.x - 1][guard_position.y] == '#' {
                    guard_position.direction = Direction::Right;
                    occurences.insert((guard_position.x, guard_position.y), 1);
                    continue;
                }

                guard_position.x -= 1;
            }
            Direction::Down => {
                if guard_position.x == rows - 1 {
                    is_in_map = false;
                    break;
                }

                if input[guard_position.x + 1][guard_position.y] == '#' {
                    guard_position.direction = Direction::Left;
                    occurences.insert((guard_position.x, guard_position.y), 1);
                    continue;
                }

                guard_position.x += 1;
            }
            Direction::Left => {
                if guard_position.y == 0 {
                    is_in_map = false;
                    break;
                }

                if input[guard_position.x][guard_position.y - 1] == '#' {
                    guard_position.direction = Direction::Up;
                    occurences.insert((guard_position.x, guard_position.y), 1);
                    continue;
                }

                guard_position.y -= 1;
            }
            Direction::Right => {
                if guard_position.y == columns - 1 {
                    is_in_map = false;
                    break;
                }

                if input[guard_position.x][guard_position.y + 1] == '#' {
                    guard_position.direction = Direction::Down;
                    occurences.insert((guard_position.x, guard_position.y), 1);
                    continue;
                }

                guard_position.y += 1;
            }
        }

        // Check if the guard is trapped
        if occurences.contains_key(&(guard_position.x, guard_position.y)) {
            // This is a turn position
            // Check if the guard has visited the same position before
            if occurences[&(guard_position.x, guard_position.y)] > 1 {
                return true;
            } else {
                // Increase the occurence of the position
                let temp = occurences
                    .get_mut(&(guard_position.x, guard_position.y))
                    .expect("Error");

                *temp += 1;
            }
        }
    }

    false
}

fn find_guard(input: &Vec<Vec<char>>) -> GuardPosition {
    let rows = input.len();
    let columns = input[0].len();

    let mut guard_position: GuardPosition = GuardPosition {
        x: 0,
        y: 0,
        direction: Direction::Up,
    };

    for i in 0..rows {
        for j in 0..columns {
            if (input[i][j] != '.') && (input[i][j] != '#') {
                match input[i][j] {
                    '^' => {
                        guard_position.x = i;
                        guard_position.y = j;
                        guard_position.direction = Direction::Up;
                    }
                    'v' => {
                        guard_position.x = i;
                        guard_position.y = j;
                        guard_position.direction = Direction::Down;
                    }
                    '<' => {
                        guard_position.x = i;
                        guard_position.y = j;
                        guard_position.direction = Direction::Left;
                    }
                    '>' => {
                        guard_position.x = i;
                        guard_position.y = j;
                        guard_position.direction = Direction::Right;
                    }
                    _ => (),
                }
            }
        }
    }

    guard_position
}
