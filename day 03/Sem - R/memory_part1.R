# part 1
library(tidyverse)

on_band <- function(number) {
  
  res <- seq(ceiling((sqrt(number) - 1) / 2), floor((sqrt(number - 1) + 1) / 2), by = 1)
  
  return(res)
  
}

band_values_and_distances <- function(band) {
  
  if (band == 0) {
    val <- 1
    dst <- 0
  } else { 
    val <- seq((2 * band - 1) ^ 2  + 1, (2 * band + 1) ^ 2, by = 1)
    if (band == 1) {
      dst_quad <- c(2 * band, band)
      dst <- rep(dst_quad, times = 4)
    } else {
      dst_quad <- c(seq(2 * band, band, by = -1), seq(band + 1, 2 * band - 1, by = 1))
      dst <- rep(dst_quad, times = 4)
    }
    dst <- c(tail(dst, -1), dst[1])
  }
  
  res <- data_frame(dist_to_origin = dst, value = val)
  
  return(res)
  
}

steps <- function(number) {
  
  b <- on_band(number)
  dst <- band_values_and_distances(b)
  
  dst %>%
    filter(value == number) %>%
    select(dist_to_origin) %>%
    .[[1]] %>%
    return()
  
}

steps(265149)