use std::io;
use std::time::Instant;

struct Point {
    x: i32,
    y: i32,
    val: i32,
}

fn main() {
    println!("Which part? <'1' or '2'>");
    let mut part = String::new();
    io::stdin().read_line(&mut part)
        .expect("Failed to read line");
    let part = part.trim();

    println!("What's your number?");
    let mut number = String::new();

    io::stdin().read_line(&mut number)
        .expect("Failed to read line");

    let number: i32 = number.trim().parse()
        .expect("Please type a number!");

    let start = Instant::now();
    if part == "1" {
        let distance: i32 = get_distance(number);
        println!("The number {} is located {} steps from the center.", number, distance);
    } else if part == "2" {
        let next_value = expand_grid(number);
        println!("Next value larger than {} was: {}.", number, next_value);
    }

    let elapsed = start.elapsed();
    let sec = (elapsed.as_secs() as f64) + (elapsed.subsec_nanos() as f64 / 1000_000_000.0);
    println!("Program took {} seconds.", sec);
}

fn get_distance(n: i32) -> i32 {
    if n == 1 {
        return 0;
    }
    let mut border:i32 = 1;
    let mut border_start:i32 = 2;

    loop {
        let items_on_border_side:i32 = border * 2 + 1;
        let items_on_border:i32 = items_on_border_side * 4 - 4;

        println!("Checking border {} (items: {} -> {})..", border, border_start, (border_start + items_on_border) - 1);
        if n >= border_start + items_on_border {
            border = border + 1;
            border_start += items_on_border;
            continue;
        }
        
        let dist_from_corner:i32 = (1 + (n - border_start)) % (items_on_border / 4);
        let dist_from_middle:i32 = (dist_from_corner - (items_on_border_side - 1) / 2).abs();
        println!("The number {} is located on border #{}, {} step(s) from the middle of its border's side.", n, border, dist_from_middle);

        return dist_from_middle + border;
    }
}

fn expand_grid(max: i32) -> i32 {
    let mut grid: Vec<Point> = vec![Point { x: 0, y: 0, val: 1}];
    let mut idx: i32 = 0;

    loop {
        let (x, y) = get_coords(idx);
        let point = calc_point(&grid, x, y);

        if point.val > max {
            println!("Ending on coord [{}, {}]..", point.x, point.y);
            return point.val;
        }

        println!("Adding point [{}, {}] with value {}..", point.x, point.y, point.val);
        grid.push(point);
        idx = idx + 1;
    }
}

fn calc_point(grid: &Vec<Point>, x:i32, y:i32) -> Point {
    let mut point = Point { x: x, y: y, val: 0 };
    for p in grid {
        if are_neighbours(&point, &p) {
            point.val = point.val + p.val;
        }
    }
    return point;
}

fn are_neighbours(a: &Point, b: &Point) -> bool {
    return (a.x - b.x).abs() <= 1 && (a.y - b.y).abs() <= 1;
}

// Got algorithm from https://stackoverflow.com/questions/19573279/algorithm-to-get-spiral-position-by-coordinate 
fn get_coords(n: i32) -> (i32, i32) {
    let mut x = 0;
    let mut y = 0;

    let v: i32 = ((n as f64 + 0.25).sqrt() - 0.5).floor() as i32;
    let spiral_base = v * (v + 1);
    let flip_flop = ((v & 1) << 1) - 1;
    let offset = flip_flop * ((v + 1) >> 1);

    x += offset;
    y += offset;

    let corner_index = spiral_base + (v + 1);

    if n < corner_index {
        x -= flip_flop * (n - spiral_base + 1);
    } else {
        x -= flip_flop * (v + 1);
        y -= flip_flop * (n - corner_index + 1);
    }

    return (x, y);
}