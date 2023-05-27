library(magrittr)

table <- read.csv2("./dane/results.csv")

table$result <- as.logical(table$result)
table$timeMs[table$timeMs == 0] <- 1

my_tibble <- tibble::tibble(table)

summary(my_tibble)

my_tibble %>% 
  ggplot2::ggplot(ggplot2::aes(clauses, timeMs, colour = log(variables))) +
  ggplot2::geom_jitter(width = 5) +
  ggplot2::scale_y_log10() +
  ggplot2::scale_x_continuous(breaks = c(0, 25000, 50000, 100000, 150000, 200000)) +
  ggplot2::geom_smooth(method='lm')

lm(log(timeMs) ~ clauses, data = my_tibble) # Cos mi tu nie gra, ale juz ide spac...
