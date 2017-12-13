input <- readLines(con = "input.txt")
input <- strsplit(x = input, split = ",")[[1]]

initial_location <- c(0, 0)
location <- initial_location

d_inf <- function(x, y) {
  max(abs(x-y))
}

max_distance <- 0

for (direction in input) {
  
  move <- switch(direction,
                 "n" = c(0, 1),
                 "s" = c(0, -1),
                 "ne" = c(1, 1),
                 "se" = c(1, 0),
                 "sw" = c(-1, -1),
                 "nw" = c(-1, 0))
  
  location <- location + move
  max_distance <- max(max_distance, d_inf(location, initial_location))
  
}

distance <- d_inf(location, initial_location)

cat("Answer part 1:", distance)
cat("Answer part 2:", max_distance)