module Probability.Model

type Probability = Probability of BigRational

let Certainly = Probability 1N
let Impossible = Probability 0N

let combine (Probability x) (Probability y) = Probability(x * y)

let toProbability x = Probability x

let inverse (Probability x) = Probability(1N - x)