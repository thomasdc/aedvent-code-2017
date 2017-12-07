m <- as.matrix(read.table(file = "input.txt"))

res_part1 <- sum(apply(m, 1, function(x) {return(abs(diff(range(x))))}))

res_part2 <- sum(apply(m, 1, function(x) {ps <- combn(sort(x), 2); p <- ps[, !(ps[2,] %% ps[1,])]; return(p[2]/p[1])}))