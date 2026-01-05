# qb.Datas
Components for datas management with scriptable objects

## CONTENT
**SerializedData<T>**

Abstract implementation of serializable data container with a GUID, storing a **<U>read only</u>** value of the specified type

**StringArray_SerializedData**

Represents a serializable data asset containing a string array, with functionality to convert the array to a single string using a specified separator.

##
**SharedData<T>**

Abstract base class for managing a shared data value with change notification, default value support, and safe event subscription handling.

**SharedDataListener<T>**

Abstract Monobehaviour base class for listening to shared data changes and invoking Unity events when the data updates.

**SharedDatasListener<T>**

Abstract base class for listening to value changes from multiple shared data providers and invoking Unity events in response.

**SDProvider<T>**
Abstract base class for providers that manage access to shared data of a specified type and resolves scriptable object reference issue from addressable assets.
>**Remarks**
>
>**A shared data can be used as a serialized field in a Monobehavior attached to a non-addressable asset, otherwise you must use an SDProvider!**


## HOW TO INSTALL

Use the Unity package manager and the Install package from git url option.

- Install at first time,if you haven't already done so previously, the package <mark>[unity-package-manager-utilities](https://github.com/sandolkakos/unity-package-manager-utilities.git)</mark> from the following url: 
  [GitHub - sandolkakos/unity-package-manager-utilities: That package contains a utility that makes it possible to resolve Git Dependencies inside custom packages installed in your Unity project via UPM - Unity Package Manager.](https://github.com/sandolkakos/unity-package-manager-utilities.git)

- Next, install the package from the current package git URL. 
  
  All other dependencies of the package should be installed automatically.

## Dependencies

- https://github.com/quanty-bandit/qb.Pattern.git
- [GitHub - codewriter-packages/Tri-Inspector: Free inspector attributes for Unity [Custom Editor, Custom Inspector, Inspector Attributes, Attribute Extensions]](https://github.com/codewriter-packages/Tri-Inspector.git)
