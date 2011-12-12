#Rules for Equality Checking in .Net


1. It is a class (reference type)
  * Does it implement IEquatable<T> - use it
  * Does it override Equals - use it
  * Reference equality check

2. It is a struct  (value type)
  * Does it override Equals - use it
  * Reflective field by field equality check
