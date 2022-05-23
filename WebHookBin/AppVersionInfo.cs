using System.Reflection;

namespace WebHookBin {
    // Adapted from https://www.hanselman.com/blog/adding-a-git-commit-hash-and-azure-devops-build-number-and-build-id-to-an-aspnet-website
    public class AppVersionInfo {
        private readonly Lazy<string> _gitHash;
        private readonly Lazy<string> _gitShortHash;

        public AppVersionInfo() {
            this._gitHash = new(() => this.DetermineGitHash());
            this._gitShortHash = new(() => this.GitHash.Substring(0, 7));
        }

        public string GitHash => this._gitHash.Value;
        public string ShortGitHash => this._gitShortHash.Value;

        private string DetermineGitHash() {
            var version = "1.0.0+LOCALBUILD"; // Dummy version for local dev
            var appAssembly = typeof(AppVersionInfo).Assembly;
            var infoVerAttr = (AssemblyInformationalVersionAttribute?)appAssembly
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute))
                .FirstOrDefault();

            if (infoVerAttr != null && infoVerAttr.InformationalVersion.Length > 6) {
                // Hash is embedded in the version after a '+' symbol, e.g. 1.0.0+a34a913742f8845d3da5309b7b17242222d41a21
                version = infoVerAttr.InformationalVersion;
            }

            return version.Substring(version.IndexOf('+') + 1);
        }
    }
}
