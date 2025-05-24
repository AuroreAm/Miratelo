naming convention for Triheroes Mighty code
- every nodes are in snake_case
- injected dependencies fields name uses the initial letter of the dependency type
- suffixes:
    - m_ : module (data only) and core (data and behavior)
    - c_ : actor controller
    - pc_ : player controller
    - ac_ : action for actors
    - t_ : action with no behavior, used to manipulate decorators
    - s_ : custom entity system