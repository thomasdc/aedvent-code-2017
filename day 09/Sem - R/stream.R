input <- readLines(con = "input.txt")
input <- strsplit(x = input, split = "")[[1]]

total_score <- 0
group_score <- 1

flag_garbage <- FALSE
flag_ignore <- FALSE

count_garbage <- 0

for (c in input) {
  
  if (flag_ignore) {
    flag_ignore <- FALSE
    next
  }
  
  if (flag_garbage) {
    
    if (c == ">") {
      flag_garbage <- FALSE
    } else if (c == "!") {
      flag_ignore <- TRUE 
    } else {
      count_garbage <- count_garbage + 1
    }
    
    next
  }
  
  if (c == "{") {
    total_score <- total_score + group_score
    group_score <- group_score + 1
  } else if (c == "}") {
    group_score <- group_score - 1
  } else if (c == "<") {
    flag_garbage <- TRUE
  }
  
}

# answer part 1
cat("Total score: ", total_score)

# answer part 2
cat("Non-canceled characters in garbage:", count_garbage)