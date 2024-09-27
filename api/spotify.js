// api/spotify.js
export default function handler(req, res) {
    const clientId = process.env.SPOTIFY_CLIENT_ID;
    const clientSecret = process.env.SPOTIFY_CLIENT_SECRET;
    const refreshToken = process.env.SPOTIFY_REFRESH_TOKEN;

    res.status(200).json({
        clientId,
        clientSecret,
        refreshToken,
    });
}
