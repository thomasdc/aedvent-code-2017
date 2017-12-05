# part 1
tiles <- read.table(file = "input.txt")[[1]]

position <- 1

steps <- 0

repeat {
  
  jump <- tiles[position]
  
  tiles[position] <- tiles[position] + 1
  
  if (jump != 0) {
    position <- position + jump
  }

  steps <- steps + 1
  
  if (position < 1 | position > length(tiles)) {
    break
  }
  
}

print(steps)

# part 2
tiles <- read.table(file = "input.txt")[[1]]

position <- 1

steps <- 0

repeat {
  
  jump <- tiles[position]
  
  if (jump >= 3) {
    tiles[position] <- tiles[position] - 1
  } else {
    tiles[position] <- tiles[position] + 1
  }
  
  if (jump != 0) {
    position <- position + jump
  }
  
  steps <- steps + 1
  
  if (position < 1 | position > length(tiles)) {
    break
  }
  
}

print(steps)