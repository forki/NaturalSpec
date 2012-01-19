module Probability.Model

type Probability = Probability of float

let Certainly = Probability 1.
let Impossible = Probability 0.

let combine x y = x