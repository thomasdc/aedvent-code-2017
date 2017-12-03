# part 2
library(tidyverse)

band_coordinates <- function(band) {
  
  if (band == 0) {
    
    coords <- data_frame(x = 0, y = 0)
  
  } else {
  
    s <- seq(-band, band, by = 1)
    coords <- bind_rows(
      data_frame(x = band, y = tail(s, -1)),
      data_frame(x = tail(rev(s), -1), y = band),
      data_frame(x = -band, y = tail(rev(s), -1)),
      data_frame(x = tail(s, -1), y = -band)
    )
  
  }
  
  res <- bind_cols(
    order = 1:nrow(coords),
    coords
  )
  
  return(res)
  
}

find_one_larger <- function(number) {
  
  band_number <- 0
  last_number <- 1
  
  band <- band_coordinates(band_number) %>%
    mutate(number = last_number)
  
  while (last_number <= number) {
    
    if (!any(is.na(band$number))) {
      
      prev_band <- band
      band_number <- band_number + 1
      band <- band_coordinates(band_number) %>%
        mutate(number = NA)
      
    }
  
    for (i in band$order) {
      
      current_x <- band %>%
        filter(order == i) %>%
        select(x) %>%
        .[[1]]
      
      current_y <- band %>%
        filter(order == i) %>%
        select(y) %>%
        .[[1]]
      
      term_prev_band <- prev_band %>% 
        filter((abs(x - current_x) <= 1) & (abs(y - current_y) <= 1)) %>%
        summarise(sum(number)) %>%
        .[[1]]
      
      term_current_band <- band %>%
        filter((order < i) & (abs(x - current_x) <= 1) & (abs(y - current_y) <= 1)) %>%
        summarise(sum(number)) %>%
        .[[1]]
      
      last_number <- term_prev_band + term_current_band
      
      band <- band %>%
        mutate(number = ifelse(order == i, last_number, number))
      
      if (last_number > number) {
        break
      }
      
    }

  }

  return(last_number)

}

find_one_larger(265149)