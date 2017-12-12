library(stringr)

create_operator <- function(operator_symbol) {
  
  if (operator_symbol == "inc") {
    operator_symbol <- "+"
  } else if (operator_symbol == "dec") {
    operator_symbol <- "-"
  }
  
  operator <- function(x, y) {
    eval(parse(text = paste0("(", x, ")", operator_symbol, "(", y, ")")))
  }
  
  return(operator)
  
}

parse_instruction <- function(instruction) {
  
  pattern_ante <- "([a-z]+) (<|>|==|!=|<=|>=) (-{0,1}[0-9]+)"
  pattern_cons <- "([a-z]+) (inc|dec) (-{0,1}[0-9]+)"
  
  ante <- str_extract(string = instruction, pattern = pattern_ante)
  cons <- str_extract(string = instruction, pattern = pattern_cons)
  
  ante_register_name <- sub(pattern = pattern_ante, replacement = "\\1", x = ante)
  ante_operator_symbol <- sub(pattern = pattern_ante, replacement = "\\2", x = ante)
  ante_arg <- sub(pattern = pattern_ante, replacement = "\\3", x = ante)
  
  cons_register_name <- sub(pattern = pattern_cons, replacement = "\\1", x = cons)
  cons_operator_symbol <- sub(pattern = pattern_cons, replacement = "\\2", x = cons)
  cons_arg <- sub(pattern = pattern_cons, replacement = "\\3", x = cons)
  
  res <- list(
    ante = list(
      name = ante_register_name,
      operator = create_operator(ante_operator_symbol),
      arg = as.numeric(ante_arg)
    ),
    cons = list(
      name = cons_register_name,
      operator = create_operator(cons_operator_symbol),
      arg = as.numeric(cons_arg)
    )
  )
  
  return(res)
  
}


lines <- readLines(con = "input.txt")

registers <- list()

largest_value <- 0

for (line in lines) {
 
  ins <- parse_instruction(line)
  
  if (!exists(ins$ante$name, where = registers)) {
    registers[[ins$ante$name]] <- 0
  }
  
  if (!exists(ins$cons$name, where = registers)) {
    registers[[ins$cons$name]] <- 0
  }
  
  op <- ins$ante$operator
  arg1 <- registers[[ins$ante$name]]
  arg2 <- ins$ante$arg
  
  if (op(arg1, arg2)) {
    
    op <- ins$cons$operator
    arg1 <- registers[[ins$cons$name]]
    arg2 <- ins$cons$arg
    
    registers[[ins$cons$name]] <- op(arg1, arg2)
    
  }
  
  largest_value <- max(largest_value, max(unlist(registers)))
  
}

# answer part 1
cat("Answer part 1: ", max(unlist(registers)))

# answer part 2
cat("Answer part 2: ", largest_value)