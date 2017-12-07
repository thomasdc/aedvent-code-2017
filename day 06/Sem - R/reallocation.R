x <- c(10, 3, 15, 10, 5, 15, 5, 15, 9, 2, 5, 8, 5, 2, 3, 6)

redistribute <- function(x) {
  
  index_max <- which.max(x)
  
  index <- (index_max + (1:x[index_max]) - 1) %% length(x) + 1
  
  x[index_max] <- 0
  x[sort(unique(index))] <- x[sort(unique(index))] + table(index)
 
  return(x) 
}

l <- list(x)

i <- 1

while (!any(duplicated(l))) {
  
  l[[i + 1]] <- redistribute(l[[i]])
  i <- i + 1
  
}

# answer part 1
print(length(l) - 1)

# answer part 2
print(length(l) - which(duplicated(l, fromLast = 1)))


