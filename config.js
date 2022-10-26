module.exports = {
    ignoreFiles: [
        'package-lock.json',
        'yarn.lock',
        'bin/*',
        'obj/*',
        'DiscordGameSDK/*'
    ],
    build: {
        overwriteDest: true,
    },
};