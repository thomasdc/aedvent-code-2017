use std::io;
use std::io::{BufReader,BufRead};
use std::fs::File;
use std::collections::HashMap;
use std::iter::Iterator;
use std::iter::FromIterator;
use std::time::Instant;
use std::thread;
use std::sync::mpsc;

fn main() {
    println!("Reading file..");    
    let (tx, rx) = mpsc::channel();
    let file = File::open("input.txt").expect("Unable to open file..");
    let lines = BufReader::new(file).lines();

    println!("Which part? <'1' or '2'>");
    let mut part = String::new();
    io::stdin().read_line(&mut part)
        .expect("Failed to read line");
    let part = part.trim();
    let mut condition_fn: fn(String) -> bool = is_valid_passphrase_unique;
    if part == "2" {
        condition_fn = is_valid_passphrase_no_anagrams;
    } 

    let start = Instant::now();
    let mut nr_of_phrases: usize = 0;
    for line in lines {
        let tx = tx.clone();
        nr_of_phrases = nr_of_phrases + 1;

        thread::spawn(move || {
            let line = line.unwrap();
            tx.send(map_condition(&condition_fn, line)).unwrap();
        });
    }

    let nr_of_valid_phrases: u32 = rx.iter().take(nr_of_phrases).sum();
    let elapsed = start.elapsed();
    let sec = (elapsed.as_secs() as f64) + (elapsed.subsec_nanos() as f64 / 1000_000_000.0);
    println!("Valid passphrases {}/{}", nr_of_valid_phrases, nr_of_phrases);
    println!("Program took {} seconds.", sec);
}

fn map_condition(f: &Fn(String) -> bool, val: String) -> u32 {
    if f(val) {
        return 1
    } else {
        return 0
    }
}

fn is_valid_passphrase_unique(passphrase: String) -> bool {
    let nr_words = passphrase.split(' ').count();
    let unique_words: HashMap<_, _> = passphrase.split(' ')
        .map(|c| (c, 0))
        .collect();

    return nr_words == unique_words.len();
}

fn is_valid_passphrase_no_anagrams(passphrase: String) -> bool {
    let nr_words = passphrase.split(' ').count();
    let unique_words: HashMap<_, _> = passphrase.split(' ')
        .map(|c| (map_to_sorted(c.to_string()), 0))
        .collect();

    return nr_words == unique_words.len();
}

fn map_to_sorted(password: String) -> String {
    let mut chars: Vec<char> = password[..].chars().collect();
    chars.sort_by(|a, b| b.cmp(a));

    String::from_iter(chars)
}