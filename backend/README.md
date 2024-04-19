# CanneTvReplay

This is the backend of CanneTV replay.

## Project setup

- Create the `cannetvjnmmain` using the dump from CanneCounter (not provided on this repo).
- Create the necessary tables using the migrations in `backend/CanneTVReplay/Migrations/`.

## Project deployment

To deploy the project in prod, build it using this command:

```sh
dotnet publish -c Release --no-self-contained --runtime linux-arm64
```

Then copy the files on the server and run the following command:

```sh
 sudo systemctl restart cannetv-replay
```