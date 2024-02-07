module.exports = {
    '**/*.cs': (filenames) => {
        const relativePaths = filenames.map((filename) => filename.split('/Zupit.DotNet/')[1]);
        return `dotnet csharpier ${relativePaths.join(' ')}`;
    },
    '**/appsettings*.json': [
        'prettier --write ',
        'sort-json'
    ]
}