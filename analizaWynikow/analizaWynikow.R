library(magrittr)

table <- read.csv2("./dane/results.csv")

table$result <- as.logical(table$result)

my_tibble <- tibble::tibble(table) %>% 
  dplyr::filter(timeMs > 0) # Usuwam te, co nie daja zadnej informacji, bo byly zbyt krotkie

summary(my_tibble)


lm(log10(timeMs) - log10(variables) ~ variables, data = my_tibble)

time_estimate <- function(n, a_log10 = -0.350414, c_log10 = 0.004781){
  (10^a_log10)*n*((10^c_log10)^n)
}

a_log10 <- -0.350414
c_log10 <- 0.004781
time_estimate(c(100, 200), a_log10, c_log10)



my_tibble %>% 
  dplyr::mutate(estimated_time = time_estimate(variables)) %>% 
  ggplot2::ggplot(ggplot2::aes(variables, timeMs)) +
  ggplot2::geom_jitter(width = 1) +
  ggplot2::scale_y_log10() +
  ggplot2::scale_x_continuous(breaks = unique(my_tibble$variables)) +
  ggplot2::geom_line(ggplot2::aes(variables, estimated_time, colour = "red"))
  

