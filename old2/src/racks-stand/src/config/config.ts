interface AppConfig {
  appName: string;
  api: {
    baseUrl: string;
    version: string;
  };
}

function getEnvVar(key: string, required = true): string {
  const value = import.meta.env[key] as string | undefined;
  if (!value && required) {
    throw new Error(`Missing environment variable: ${key}`);
  }
  return value ?? '';
}

export const config: AppConfig = {
  appName: getEnvVar('VITE_APP_NAME'),
  api: {
    baseUrl: getEnvVar('VITE_API_BASE_URL'),
    version: getEnvVar('VITE_API_VERSION'),
  },
};
