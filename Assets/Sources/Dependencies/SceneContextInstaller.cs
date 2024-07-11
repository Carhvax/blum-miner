public class SceneContextInstaller : EntryUIKitInstaller {

    protected override void BindServices() {
        Container.BindAsSingle<AdService>();
    }

    protected override void OnBindTargetInstances() {
        
    }

}