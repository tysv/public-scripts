#!/bin/bash
#cd & ls combined
#fuzzy searches working directory for matches
#searches up the absolute path for a match -- could fuzzify this too
#uses returns instead of exit due to source requirement

(return 0 2>/dev/null) || { echo "ERROR: cs need to be sourced" && exit 0; }

exiting () { 
	unset debugflag fullpath path relative findstart 
	unset destination maybe potentials buildback
	unset -f debug error teleport exiting
	trap - EXIT 
	trap - ERR
}
trap 'exiting' EXIT ERR

variable_hiding_wrapper () {
	local debugflag=false
	debug () { [ "$debugflag" = true ] && echo "$1"; }
	error () { printf "error: %s\n" "$1"; return "${2:-1}"; }

	if [ ! "$#" -eq 1 ]; then
		echo ">>>  cs - combines cd and ls into one command - traverse locally  <<<"
		echo -e "\tcs <num> means \"cd ..\" num times."; return 2
	fi

	################################################################################

	teleport () {
		if [ -n "$1" ] && [ -d "$1" ]; then builtin cd "$1" && { ls; return 0; }; fi;
	}

	local fullpath="$(realpath "$1")"
	local path="$(basename "$1")"
	local relative="${1%[/]*}"; local here='./'
	if [ "$relative" = "$1" ]; then local findstart="$here"
		else local findstart="$relative"
	fi
	debug "findstart=>$findstart<"



	if [ -d "$fullpath" ]; then
		teleport "$fullpath"; debug "\$fullpath teleport"
	elif [ -z "${path//[0-9]/}" ] || [ "${path//[0-9]/}" = '..' ]; then
			destination="$(strbuilder -r "${path//[!0-9]}" "../")"
			teleport "$destination" &&  return $?
	else
		#tries to auto-complete given the failed key
		##FIRST## try cs test 	directories inside current working directory
		shopt -s nullglob; IFS=$'\n\t '
		local maybe=(); while read -r line; do if [ -n "$line" ]; then maybe+=("$line"); fi
		done <<< "$(find "$findstart" -maxdepth 1 -type d -name "$path*" -o -name ".$path*")"
		if [ "${#maybe[@]}" -eq 1 ] && [ -n "${maybe[0]}" ]; then 
			teleport "${maybe[0]}" && return $?; fi
		##SECOND## try going back up the list and matching directories that way
		IFS='/' read -r -a potentials <<< "$PWD"
		local buildback=''
		for potent in $(seq 0 "${#potentials[@]}"); do
			debug "p:$path		[i]:${potentials[$ind]}"
			if [ "$path" = "${potentials[$potent]}" ]; then 
				teleport "$buildback$path" && return $?; fi
			buildback+="${potentials[$potent]}/"
			debug "\$buildback $buildback"
		done
		#echo ${maybe[@]} old debugging coment left in??
		if [ "${#maybe[@]}" -gt 1 ]; then
			maybe+=("exit_quit_leave_do_nothing")
			teleport "$(echo "${maybe[@]}" | tr $' ' $'\n' | fzf)" && return
			#printf "%s\t" "${maybe[@]}"; echo; return 1
		else
			echo "cs: fail: ($path) doesn't look like a valid destination, chief."
			return 2
		fi
	fi
}
variable_hiding_wrapper "$@"; set --
trap - EXIT 
trap - ERR
