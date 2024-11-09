# Stage 1: Compile and Build angular codebase

# Use official node image as the base image
FROM node:latest AS build

# setup biến môi trường
ARG BUILD_ENV_ARG

# Set the working directory
WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build${BUILD_ENV_ARG}

# Stage 2: Serve app with nginx server

# Use official nginx image as the base image
FROM nginx:latest

# Copy the build output to replace the default nginx contents.
COPY --from=build /app/dist /usr/share/nginx/html

# Copy NGINX configuration file
COPY default.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80