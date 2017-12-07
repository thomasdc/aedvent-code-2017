# part 1
trampolines <- read.table(file = "input.txt")[[1]]

position <- 1

steps <- 0

repeat {
  
  jump <- trampolines[position]
  
  trampolines[position] <- trampolines[position] + 1
  
  if (jump != 0) {
    position <- position + jump
  }

  steps <- steps + 1
  
  if (position < 1 | position > length(trampolines)) {
    break
  }
  
}

print(steps)

# part 2
trampolines <- read.table(file = "input.txt")[[1]]

position <- 1

steps <- 0

repeat {
  
  jump <- trampolines[position]
  
  if (jump >= 3) {
    trampolines[position] <- trampolines[position] - 1
  } else {
    trampolines[position] <- trampolines[position] + 1
  }
  
  if (jump != 0) {
    position <- position + jump
  }
  
  steps <- steps + 1
  
  if (position < 1 | position > length(trampolines)) {
    break
  }
  
}

print(steps)