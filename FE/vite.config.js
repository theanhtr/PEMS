import { defineConfig, loadEnv } from "vite";
import { resolve } from "path";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig(({ command, mode }) => {
  const env = loadEnv(mode, process.cwd());

  return {
    server: {
      port: 6789,
    },
    plugins: [
      vue()
    ],
    resolve: {
      alias: {
        "@": resolve(__dirname, "src"),
      },
    },
    base:
      command === "build"
        ? `${env.VITE_PREFIX_CDN_URL}`
        : "/"
  }
});
