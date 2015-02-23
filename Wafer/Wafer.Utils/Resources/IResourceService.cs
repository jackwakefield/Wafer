using Wafer.Core;

namespace Wafer.Utils.Resources {
    public interface IResourceService {
        void LoadValues();

        bool IsResourceIdentifier(string value);

        Dimension GetDimensionByIdentifier(string value);

        string GetStringByIdentifier(string value);

        int GetIntegerByIdentifier(string value);

        Colour GetColourByIdentifier(string value);
    }
}
