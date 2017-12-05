library(tidyverse)

# part 1
passphrases <- readLines(con = "input.txt")

is_valid_part1 <- function(passphrase) {

  words <- strsplit(x = passphrase, split = " ")[[1]]
  
  words_count <- table(words, dnn = FALSE)
  
  return(all(words_count == 1))
  
}

res_part1 <- sum(sapply(passphrases, is_valid_part1))

print(res_part1)

# part 2

sort_letters <- function(word) {
  
  strsplit(x = word, split = "")[[1]] %>%
    sort() %>%
    paste(collapse = "")
  
}

is_valid_part2 <- function(passphrase) {
  
  words <- strsplit(x = passphrase, split = " ")[[1]]
  
  words_std <- sapply(words, sort_letters)
  
  words_std_count <- table(words_std, dnn = FALSE)
  
  return(all(words_std_count == 1))
  
}

res_part2 <- sum(sapply(passphrases, is_valid_part2))

print(res_part2)
