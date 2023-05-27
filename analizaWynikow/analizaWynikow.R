library(magrittr)

table <- read.csv2("./dane/results.csv")

table$result <- as.logical(table$result)

my_tibble <- tibble::tibble(table) %>% 
  dplyr::filter(timeMs > 0) # Usuwam te, co nie daja zadnej informacji, bo byly zbyt krotkie

summary(my_tibble)


lm(log10(timeMs) - log10(variables) ~ variables, data = my_tibble)

a_log10 <- -0.350414
c_log10 <- 0.004781

time_estimate <- function(n, a_log10 = -0.350414, c_log10 = 0.004781){
  (10^a_log10)*n*((10^c_log10)^n)
}


g <- my_tibble %>% 
  ggplot2::ggplot(ggplot2::aes(variables, timeMs)) +
  ggplot2::geom_jitter(ggplot2::aes(color = "Data Points"), width = 1) +
  ggplot2::scale_y_log10(breaks = c(1, 100, 10000), labels = c("1 ms", "100 ms", "10 s")) +
  ggplot2::scale_x_continuous(breaks = unique(my_tibble$variables)) +
  ggplot2::geom_function(ggplot2::aes(color = "Time Estimate"), fun = time_estimate) +
  ggplot2::ggtitle("Czas potrzebny na rozwiązanie problemu 3SAT") +
  ggplot2::xlab("Liczba zmiennych w formule (n)") + ggplot2::ylab("Czas") +
  ggplot2::labs(colour = "", subtitle = "pominięto obserwacje dla ktorych czas = 0 ms") +
  ggplot2::scale_color_manual(
    labels = c("Obserwacje",
               expression(0.45 * italic("n") * (1.011)^n)),
    values = c("black", "red")
  ) +
  ggplot2::theme(
    plot.title = ggplot2::element_text(size=14, face="bold"),
    axis.title.x = ggplot2::element_text(size=14, face="bold"),
    axis.title.y = ggplot2::element_text(size=14, face="bold"),
    panel.grid.minor.x = ggplot2::element_blank()
  )

g

ggplot2::ggsave("wykres_czasu.png", g, width = 11, height = 5)



# Wielkosc

# najdluzej liczacy sie problem
my_tibble[my_tibble$timeMs > 1231840,]

median(my_tibble[my_tibble$variables == 100,]$timeMs) # 120
median(my_tibble[my_tibble$variables == 150,]$timeMs) # 285
median(my_tibble[my_tibble$variables == 200,]$timeMs) # 994


max(my_tibble[my_tibble$variables == 100,]$timeMs) # 4271




