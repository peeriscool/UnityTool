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
            location = _location;
            transform = _transform;

        }
        public void Execute()
        {
            Oldlocation = transform.position;
            transform.position = location;
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
            GameObject.Instantiate(Myobject);
        }

        public void Undo()
        {
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

            //lambda expresion Anonimus function
            //https://learn.microsoft.com/nl-nl/dotnet/csharp/language-reference/operators/lambda-expressions
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
            else //make empty filter
            {
                //try  to get from child
                if (ImportedObject.GetComponentInChildren<MeshFilter>())
                {
                    filter = ImportedObject.GetComponentInChildren<MeshFilter>();
                }
                else
                {
                    Debug.Log("Adding meshfilter to root");
                    filter = ImportedObject.AddComponent<MeshFilter>();
                }
            }
            ExtensionMethods.AddMeshCollider(ImportedObject);
            //GenerateBoxcolliderOnMesh(ImportedObject, filter);
            JsonFileToProject.ProjectFile.SceneObjects.Add(new GameObjectInScene(ImportedObject, filter));
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}


