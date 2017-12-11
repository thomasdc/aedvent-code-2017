l <- c(147,37,249,1,31,2,226,0,161,71,254,243,183,255,30,70)

s <- 0:255

n <- length(s)

index <- 1

skip <- 1

for (l_cur in l) {
  
  i <- (index + 0:(l_cur - 1) - 1) %% n + 1
  s[i] <- rev(s[i])
  
  index <- (index + (skip + l_cur - 1) - 1) %% n + 1
  skip <- skip + 1
  
  print(s)
}

# answer part 1:
cat("Product first two digits: ", s[1] * s[2])