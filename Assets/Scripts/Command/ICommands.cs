using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class ICommands
{
    [System.Serializable]

    public class IMove : ICommand
    {
        ObjectCommander playerMover;
        Vector3 movement;
        public IMove(ObjectCommander player, Vector3 moveVector)
        {
            this.playerMover = player;
            this.movement = moveVector;
        }
        public void Execute()
        {
            playerMover.Move(movement);
            Debug.Log(playerMover.transform.position);
        }
        public void Undo()
        {
            playerMover.Move(-movement);
        }
        public class CameraNavigation : ICommand
        {
            FlyCamera MyFlycam;
            GameObject Owner;
            CameraNavigation(FlyCamera script, GameObject _Owner)
            {
                MyFlycam = script;
                Owner = _Owner;
            }
            public void Execute()
            {
                MyFlycam.enabled = true;
            }

            public void Undo()
            {
                MyFlycam.enabled = false;
            }
        }
    }
    public class MoveCommand : ICommand
    {
        Transform transform;
        Vector3 direction;
        public MoveCommand(Vector3 _direction, Transform _transform)
        {
            direction = _direction;
            transform = _transform;

        }
        public void Execute()
        {
            transform.position += direction;
        }

        public void Undo()
        {
            transform.position -= direction;
        }
    }
    public class SetPositionCommand : ICommand
    {
        Transform transform;
        Vector3 Oldlocation;
        Vector3 location;
        public SetPositionCommand(Vector3 _location, Transform _transform)
        {
            if (_transform != null)
            {
                location = _location;
                transform = _transform;
            }
            //object is rendered useless  without transform
        }
        public void Execute()
        {
            if(transform != null)
            {
                Oldlocation = transform.position;
                transform.position = location;
            }
        }

        public void Undo()
        {
            transform.position = Oldlocation;
        }
    }
    public class SetRotationCommand : ICommand
    {
        Transform transform;
        Quaternion rotation;
        Quaternion Oldrotation;
        public SetRotationCommand(Quaternion _rotation, Transform _transform)
        {
            rotation = _rotation;
            transform = _transform;

        }
        public void Execute()
        {
            Oldrotation = transform.rotation;
            transform.rotation = rotation;
        }

        public void Undo()
        {
            transform.rotation = Oldrotation;
        }
    }
    public class CreatePrefab : ICommand
    {
        //fetch object from resources.load By name
        GameObject Myobject;
        public CreatePrefab(GameObject reference)
        {
            Myobject = reference;
        }
        public CreatePrefab(string ResourceName) //should check if resource exists
        {
            Myobject = Resources.Load(ResourceName) as GameObject;
        }
        public void Execute()
        {
            Myobject = GameObject.Instantiate(Myobject);
        }

        public void Undo()
        {
            //Myobject.active = false;
           GameObject.Destroy(Myobject);
        }
    }
    public class ExportCommand : ICommand
    {
        public ExportCommand()
        {

        }
        public void Execute()
        {
            List<GameObject> exportmeshes = new List<GameObject>();
            for (int i = 0; i < JsonFileToProject.ProjectFile.SceneObjects.Count; i++)
            {
                if (JsonFileToProject.ProjectFile.SceneObjects[i].Getrefrence() != null) exportmeshes.Add(JsonFileToProject.ProjectFile.SceneObjects[i].Getrefrence());
            }
            string path = openProjectFile();
            ObjExporterStandalone exportUtil = new ObjExporterStandalone();
            exportUtil.Export(path, exportmeshes);
        }

        public void Undo()
        {
            //not sure what to place here yet
        }

        string openProjectFile()
        {
            string sendpath = "";
            var bp = new AnotherFileBrowser.Windows.BrowserProperties();
            bp.filter = "obj files (*.obj)|*.obj|All Files (*.*)|*.*";
            bp.filterIndex = 0;

            new AnotherFileBrowser.Windows.FileBrowser().SaveFileBrowser(bp, "MyLevelExport", bp.filter, path =>
            {
                //   Load Binary or Json format of project
                Debug.Log(path);
                Debug.Log("Load Json project");
                sendpath = path;

            });
            return sendpath;
        }
    }
    public class SaveCommand : ICommand
    {
        public SaveCommand()
        {

        }
        public void Execute()
        {
            var bp = new AnotherFileBrowser.Windows.BrowserProperties();
            bp.filter = "Json files (*.Json)|*.Json|All Files (*.*)|*.*";
            bp.filterIndex = 0;

            new AnotherFileBrowser.Windows.FileBrowser().SaveFileBrowser(bp, "ProjectFile_01", ".json", SavePath =>
            {
                JsonFileToProject.ProjectFile.ProjectName = Path.GetFileName(SavePath);
                JSONSerializer.Save(SavePath, JsonFileToProject.ProjectFile);

            });
        }

        public void Undo()
        {

        }
    }
    public class ImportCommand : ICommand
    {
        public ImportCommand()
        {

        }
        public void Execute()
        {
            GameObject ImportedObject = new GameObject();
            //Action<string> action = (pat);
            Dummiesman.OBJLoader ObjModel = new Dummiesman.OBJLoader();
            var bp = new AnotherFileBrowser.Windows.BrowserProperties();
            bp.filter = "Obj files (*.obj)|*.obj|All Files (*.*)|*.*"; //TODO:mtl
            bp.filterIndex = 0;

            new AnotherFileBrowser.Windows.FileBrowser().OpenFileBrowser(bp, path =>
            {
                Debug.Log("Loading" + bp + " Json project from" + path);
                ImportedObject = ObjModel.Load(path);
            });

            MeshRenderer render = ImportedObject.AddComponent<MeshRenderer>();
            MeshFilter filter;

            if (ImportedObject.TryGetComponent<MeshFilter>(out MeshFilter _filter)) //check if mesh comes with meshfilter
            {
                filter = _filter;
            }
            else 
            {
                //try to get from child
                if (ImportedObject.GetComponentInChildren<MeshFilter>())
                {
                    filter = ImportedObject.GetComponentInChildren<MeshFilter>();
                }
                else //make empty filter
                {
                    Debug.Log("Warning: Adding Empty mesh filter!");
                    filter = ImportedObject.AddComponent<MeshFilter>();
                }
            }
            
	        ImportedObject.AddComponent<MeshFilter>().mesh = filter.mesh;
            Debug.Log("Transform childs" + ImportedObject.transform.childCount);
            for (int i = 0; i < ImportedObject.transform.childCount; i++)
            {
                Transform child= ImportedObject.transform.GetChild(i);
                JsonFileToProject.ProjectFile.SceneObjects.Add(new GameObjectInScene(child));
            }
            ExtensionMethods.AddMeshCollider(ImportedObject);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
    public class OpenProjctFile : ICommand
    {
            //gets called by the load buttonclick
        
        public void Execute()
        {
            //(ProjectScene.name == "empty") //we still have an unused scene with possible use
            //{
            //    SceneManager.UnloadSceneAsync(ProjectScene);
            //}
            var bp = new AnotherFileBrowser.Windows.BrowserProperties();
            bp.filter = "Json files (*.Json)|*.Json|All Files (*.*)|*.*"; //TODO: Implement Json
            bp.filterIndex = 0;

            new AnotherFileBrowser.Windows.FileBrowser().OpenFileBrowser(bp, path =>
            {
                Debug.Log("Loading" + bp + " Json project from" + path);
                ProjectData MyFile = JSONSerializer.Load<ProjectData>(path); //contains file name and extention
                //  StartProjectFromJson(MyFile.ProjectName);
                JsonFileToProject.LoadData(MyFile); //set project data instance
            });
        }
    

        public void Undo()
        {
            
        }

     
    }
    public class NewFile : ICommand
    {
        UnityEngine.SceneManagement.Scene ProjectScene;
        string ProjectName;
        public NewFile(UnityEngine.SceneManagement.Scene _ProjectScene, string _ProjectName)
        {
            ProjectScene = _ProjectScene;
            ProjectName = _ProjectName;
        }
        //UnityEngine.SceneManagement.Scene
        public void Execute()
        {
             //should be divided in to different load functions for Json and creating a new project
                if (ProjectName == "Empty") //If its a new file populate with serializable objects
                {
                    if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName("Empty").isLoaded)
                    {
                        ProjectScene = UnityEngine.SceneManagement.SceneManager.CreateScene(ProjectName); //should use a version number
                        UnityEngine.SceneManagement.SceneManager.SetActiveScene(ProjectScene);
                        JsonFileToProject.ProjectFile = new ProjectData();
                        JsonFileToProject.ProjectFile.Version = Mathf.Round(0.1f);
                        JsonFileToProject.ProjectFile.SceneObjects = new List<GameObjectInScene>();

                        ExtensionMethods.MakeStartCube(ProjectScene);
                        //ExtensionMethods.CreateManager(ProjectScene);
                        ExtensionMethods.makebaseplane(ProjectScene);
                    }
                    else  //Toggle Back to Menu
                    {
                 //       Menu.rootVisualElement.visible = false;
                    }
                }

                else //we already should have the data from Json
                {
                    Debug.Log("Opening: " + ProjectName);

                    for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
                    {
                        if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name == ProjectName)  //check if scene already exists
                        {
                            //Can not create scene until old one is removed
                            Debug.Log("Unloading: " + UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name);
                            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(ProjectScene);
                            ProjectName = "Empty";
                            JsonFileToProject.ProjectFile = new ProjectData();
                        }
                    }

                    if (ProjectName != null)
                    {
                        ProjectScene = UnityEngine.SceneManagement.SceneManager.CreateScene(ProjectName); //should use a file version number
                        UnityEngine.SceneManagement.SceneManager.SetActiveScene(ProjectScene);
                    }
                   // ExtensionMethods.CreateManager(ProjectScene);
                    ExtensionMethods.makebaseplane(ProjectScene);
                }
              //  toggle();
                FlyCamera.Instance.enabled = true;
                UnityEngine.Cursor.visible = false;
            

        }

        public void Undo()
        {
          
        }
    }
}


