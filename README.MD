A cross-platform C# application targeting native Android & iOS, using Xamarin with a Google Firebase backend for Database & Authentication.

# The Test
This project started off as a personal hackathon. It was meant to test my ability to move fast with unfamiliar toolsets.
The deadline was 2 weeks, and required learning:
 - Google Maps API
 - Google Services API security (admin, restrictions, etc.)
 - [MVVM](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel)
 - Windows Presentation Forms & Data Binding
 - Xamarin Forms navigation
 - Dependency Services for native features (such as maps)
 - Firebase Realtime Database
 - Authentication APIs (token generation)
 - Visual Studio package management
 - NuGet dependency resolution

**Bonus Challenge:**
Had to work with [undocumented API](https://github.com/xamarin/GooglePlayServicesComponents) for wrapping Firebase native calls into a Xamarin project (reading [Java docs](https://firebase.google.com/docs/database/android/read-and-write) for C# API). Interpreting one API to the other is easy, so long as you know where to look.

# The Result 
The product has a fully working feature-set. There is only a fraction of the app's design available, however the required underlying tech is in place to support further development & polish.

Sample views of the forms recorded from a **Galaxy Note 8** smartphone *(click GIF to download video for playback control)*:

[![Alt text](https://github.com/kilogold/Beachers/blob/master/Readme/BeachersProto.gif?raw=true)](https://github.com/kilogold/Beachers/raw/master/Readme/BeachersProto.mp4)

## Integrating Firebase Authentication:
![enter image description here](https://github.com/kilogold/Beachers/raw/master/Readme/Shot1.png)

## Database also using Firebase (RealtimeDB):
```json
{
  "Beachers" : {
    "c4VxCXY4bae1jGAnQceshXEoQS62" : {
      "name" : "Alex Joel",
      "phone" : "7876478657"
    }
  },
  "Booking" : {
    "6p8v3anTseXRP06bcxmS5dwXNNQ2" : {
      "2020-08-02T16:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0, 1 ], [ 2 ] ],
        "gear" : [ "4548a6d4-88fb-4a71-a122-2bda39db9be8", "5f61e89b-8200-4912-bf43-fd50654e351e", "7470eb97-80a2-40ef-ac56-28afb7520d72" ],
        "lengthMinutes" : 10,
        "location" : "119.6824,23.56811"
      },
      "2020-08-02T21:30:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0 ], [ 1, 2 ] ],
        "gear" : [ "4548a6d4-88fb-4a71-a122-2bda39db9be8", "5f61e89b-8200-4912-bf43-fd50654e351e", "7470eb97-80a2-40ef-ac56-28afb7520d72" ],
        "lengthMinutes" : 10,
        "location" : "-121.5121,45.71598"
      },
      "2020-08-03T15:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0, 1 ], [ 2 ] ],
        "gear" : [ "4548a6d4-88fb-4a71-a122-2bda39db9be8", "5f61e89b-8200-4912-bf43-fd50654e351e", "7470eb97-80a2-40ef-ac56-28afb7520d72" ],
        "lengthMinutes" : 10,
        "location" : "119.6824,23.56811"
      },
      "2020-08-08T19:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0 ], [ 1, 2, 3 ] ],
        "gear" : [ "-MDh5ip4QAXtVn8YBoTp", "4548a6d4-88fb-4a71-a122-2bda39db9be8", "5f61e89b-8200-4912-bf43-fd50654e351e", "7470eb97-80a2-40ef-ac56-28afb7520d72" ],
        "lengthMinutes" : 480,
        "location" : "-121.5121,45.71598"
      },
      "2020-08-13T15:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0, 1 ] ],
        "gear" : [ "4548a6d4-88fb-4a71-a122-2bda39db9be8", "7470eb97-80a2-40ef-ac56-28afb7520d72" ],
        "lengthMinutes" : 10,
        "location" : "-121.5121,45.71598"
      },
      "2020-08-29T19:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0 ] ],
        "gear" : [ "7470eb97-80a2-40ef-ac56-28afb7520d72" ],
        "lengthMinutes" : 60,
        "location" : "-67.06495,18.51387"
      },
      "2020-10-08T15:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0, 1 ], [ 2 ] ],
        "gear" : [ "4548a6d4-88fb-4a71-a122-2bda39db9be8", "5f61e89b-8200-4912-bf43-fd50654e351e", "7470eb97-80a2-40ef-ac56-28afb7520d72" ],
        "lengthMinutes" : 60,
        "location" : "-121.5121,45.71598"
      }
    },
    "Fdb1SYkejqQICbMPn6CB93Kjg4m1" : {
      "2020-08-20T11:00:00Z" : {
        "beacher" : "",
        "lengthMinutes" : 300,
        "location" : "-67.06495,18.51387"
      }
    },
    "VoeRMTUAIRcsiMk10OKTQbnO1u73" : {
      "2020-08-09T15:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0, 1 ] ],
        "gear" : [ "-MDltu2W0_Blx6mdjJK8", "-MDlu0nfnKPXI8Rh4xWw" ],
        "lengthMinutes" : 600,
        "location" : "-121.5121,45.71598"
      }
    },
    "fMuUNeSqWxYFBrmQgA2jOYkQFKX2" : {
      "2020-08-21T17:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0, 1 ], [ 2 ] ],
        "gear" : [ "-MDw6KCixBUgGhL9jqNF", "-MDw6ScPtVzJQN-qGHGm", "-MDw6Z_2qOGyDiRsL-Ny" ],
        "lengthMinutes" : 480,
        "location" : "-121.5121,45.71598"
      }
    },
    "xtpoMwjPFtfDbLG4GHznSkPfGrx2" : {
      "2020-08-15T15:00:00Z" : {
        "beacher" : "",
        "deployments" : [ [ 0, 1 ] ],
        "gear" : [ "-MDkkWLKjKXCeS6UNIIr", "-MDkkuEl5M2XtV1QyvB_" ],
        "lengthMinutes" : 600,
        "location" : "-121.5121,45.71598"
      }
    }
  },
  "History" : {
    "6p8v3anTseXRP06bcxmS5dwXNNQ2" : {
      "2020-07-17T05:29:24Z" : {
        "Activity" : "Session",
        "BeforeDeployment" : "https://i.redd.it/sdc2xzkbr4151.jpg",
        "StorageDrop" : "https://i.redd.it/cbo8xn08mqy41.jpg",
        "StoragePickup" : "https://i.redd.it/cbo8xn08mqy41.jpg"
      },
      "2020-08-07T05:29:24Z" : {
        "Activity" : "Maintenance",
        "Order Request" : "Bladder replacement. Please use Airtime valve.",
        "StorageDrop" : "https://i.redd.it/cbo8xn08mqy41.jpg",
        "StoragePickup" : "https://i.redd.it/cbo8xn08mqy41.jpg"
      }
    }
  },
  "Inventory" : {
    "6p8v3anTseXRP06bcxmS5dwXNNQ2" : {
      "4548a6d4-88fb-4a71-a122-2bda39db9be8" : {
        "brand" : "Core",
        "memo" : "The best kite evar!",
        "model" : "XR6",
        "size" : "9m",
        "type" : "Kite"
      },
      "5f61e89b-8200-4912-bf43-fd50654e351e" : {
        "boots" : false,
        "brand" : "Wainman Hawaii",
        "memo" : "My first board",
        "model" : "Joke",
        "size" : "138cm",
        "style" : "Twintip",
        "type" : "Board"
      },
      "7470eb97-80a2-40ef-ac56-28afb7520d72" : {
        "brand" : "Core",
        "memo" : "Ol' reliable",
        "model" : "XR6",
        "size" : "12m",
        "type" : "Kite"
      }
    },
    "Fdb1SYkejqQICbMPn6CB93Kjg4m1" : {
      "-MDkykDoBkeW41vIJNKy" : {
        "brand" : "Konoha",
        "memo" : "Test",
        "model" : "Hokage",
        "size" : "6",
        "type" : "Kite"
      },
      "-MDkyq7DMuH0kCHYSy_N" : {
        "brand" : "Borinquen",
        "memo" : "Mofongo",
        "model" : "Rico Sandwich",
        "size" : "10",
        "type" : "Board"
      }
    },
    "VoeRMTUAIRcsiMk10OKTQbnO1u73" : {
      "-MDltu2W0_Blx6mdjJK8" : {
        "brand" : "Core",
        "memo" : "My first kiss",
        "model" : "XR6",
        "size" : "12",
        "type" : "Kite"
      },
      "-MDlu0nfnKPXI8Rh4xWw" : {
        "brand" : "Walkman",
        "memo" : "Craigslist buy",
        "model" : "Slash",
        "size" : "138",
        "type" : "Board"
      }
    },
    "fMuUNeSqWxYFBrmQgA2jOYkQFKX2" : {
      "-MDw6KCixBUgGhL9jqNF" : {
        "brand" : "Core",
        "memo" : "My favorite kite",
        "model" : "XR6",
        "size" : "12",
        "type" : "Kite"
      },
      "-MDw6ScPtVzJQN-qGHGm" : {
        "brand" : "Wainman Hawaii",
        "memo" : "The first board I've ever owned.",
        "model" : "Joke",
        "size" : "138",
        "type" : "Board"
      }
    },
    "xtpoMwjPFtfDbLG4GHznSkPfGrx2" : {
      "-MDkkWLKjKXCeS6UNIIr" : {
        "brand" : "Wainman Hawaii",
        "memo" : "My first kite",
        "model" : "Boss",
        "size" : "12",
        "type" : "Kite"
      },
      "-MDkkuEl5M2XtV1QyvB_" : {
        "brand" : "North",
        "memo" : "New board I got for Christmas.",
        "model" : "Excalibur",
        "size" : "138",
        "type" : "Board"
      }
    }
  },
  "Users" : {
    "59040ad7e3f33022d96d84348726ddb5" : {
      "name" : "JohnDoe",
      "phone" : "9396426333"
    },
    "6p8v3anTseXRP06bcxmS5dwXNNQ2" : {
      "name" : "Kelvin Bonilla",
      "phone" : "7879664747"
    }
  }
}
```
