library(gtools)

input <- "147,37,249,1,31,2,226,0,161,71,254,243,183,255,30,70"
input <- strsplit(x = input, split = "")[[1]]

suffix <- c(17, 31, 73, 47, 23)

l <- c(asc(input), suffix)
names(l) <- NULL

s <- 0:255

n <- length(s)

index <- 1

skip <- 1

for (k in 1:64) {
  
  for (l_cur in l) {
    
    i <- (index + 0:(l_cur - 1) - 1) %% n + 1
    s[i] <- rev(s[i])
    
    index <- (index + (skip + l_cur - 1) - 1) %% n + 1
    skip <- skip + 1
    
  }

}

s_blocks <- split(s, rep(1:16, each = 16))

dense_hash <- sapply(s_blocks, function(x){Reduce(bitwXor, x)})
dense_hash <- paste(as.hexmode(dense_hash), collapse = "")

# answer part 2
cat("Dense hash:", dense_hash)