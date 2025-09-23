### **Lyra Framework – Coding Guide**

Lyra is my RPG character framework built on top of Unity.
It have its own life cycle system, deterministic code order, and an atomic system with forced dependency injection, created for Triheroes.

## General Naming & Style Rules

Lyra source code does not follow .NET naming or style conventions.

Non-Lyra code (e.g., Unity-specific classes, MonoBehaviour not extending Lyra, Unity custom editors, or extension classes provided by Lyra) stay following conventional C# style.

- All Lyra classes, fields, methods, and properties use snake_case.

- Fields & Methods

Fields starting with underscore _ represent:
Method arguments, or
Passed arguments stored in instances or statics, but not used for the class lifetime.

Methods starting with underscore (_) are events or callbacks.
Example: instead of on_destroy, use _destroy.

Virtual methods (used to override sealed base virtual methods) must have extra underscores.
Example: _start() → __start() in virtus.

- Dependency Injection

Injected field names should match their type whenever possible.
Example: a Character type becomes character.

Important injected fields can be shortened to single letters.
Example:
character → c
axeal → a

Use [link] (dependency injection) only for truly required components.

[link] will automatically create the needed component if it does not exist.

- Special Methods

A mandatory method that returns the class instance must be named _.
Example: dash._() returns the dash instance, used as argument for motor act.

- Superstar Classes

Superstar classes (manager classes used by phoenix) must have single-word names.
Example: the default PoolManager of Lyra is named orion.

- Atomic Base Classes

Each atomic base class serves a specific role:
Moon → Data
Star → Permanent system
Action → Sequence-based tasks (self-stopping)
Ink → Data transfer

- Attributes & Data Flow

Use [inked] for components that require initial data (from Inspector or custom editors).
This signals that some fields are populated externally.

Use [export] only for initial data transfer.
Runtime fields must remain private.

- Coding Style

Always K&R bracket style for Lyra code.

Public members should use shorter names.

- For public methods on Star components:

Instead of wrapping code with if (on) { … }
Use early exit style:
```
if (!on) return;
```
This makes it explicit that the codes in there are for alive stars.

Custom callbacks for stars have to take account of > on before being invoked.